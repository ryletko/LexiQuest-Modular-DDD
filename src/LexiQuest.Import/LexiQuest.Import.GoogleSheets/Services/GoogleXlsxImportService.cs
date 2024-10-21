using System.Text;
using LexiQuest.Shared.Puzzle;
using OfficeOpenXml;

namespace LexiQuest.Import.GoogleSheets.Services;

internal class GoogleXlsxImportService(IGoogleExcelProvider googleExcelProvider)
{
    private class ImportedRow
    {
        public int Index { get; set; }
        public string?[] RowData { get; set; }
    }

    private readonly RowFormat _format = RowFormat.Default;

    public async IAsyncEnumerable<ImportedWord> ImportAsync(string url, CancellationToken cancellationToken = default)
    {
        await using (var s = await googleExcelProvider.GetGoogleExcel(url, cancellationToken))
        {
            using (var xlApp = new ExcelPackage(s))
            {
                var xlWorkbook = xlApp.Workbook;
                var xlWorksheet = xlWorkbook.Worksheets[0];

                var rowCount = xlWorksheet.Dimension.End.Row;
                var colCount = xlWorksheet.Dimension.End.Column;

                for (int i = 0; i < rowCount; i++)
                {
                    if (cancellationToken.IsCancellationRequested) break;

                    if (i == 0) // title
                        continue;

                    var row = new string[colCount];
                    for (int j = 0; j < colCount; j++)
                    {
                        row[j] = (string) xlWorksheet.Cells[i + 1, j + 1].Value;
                    }

                    if (!String.IsNullOrWhiteSpace(row[1]))
                    {
                        var importedRow = new ImportedRow()
                                          {
                                              Index   = i,
                                              RowData = row
                                          };
                        var importedWord = ReadRow(importedRow);
                        yield return importedWord;
                    }
                }
            }
        }
    }

    private ImportedWord ReadRow(ImportedRow row)
    {
        var rowData = row.RowData;
        var rowLength = rowData.GetLength(0);

        var flags = rowData[0];
        var partOfSpeech = ParsePartOfSpeech(rowData[1]);
        var foreignWord = rowData[2];

        var synonims = new List<string>();
        if (rowLength > _format.SynonimColNum)
        {
            var synonimsStr = rowData[_format.SynonimColNum];
            if (!String.IsNullOrEmpty(synonimsStr))
                synonims.AddRange(synonimsStr.Split(',', '|').Select(s => s.Trim()));
        }

        var example = _format.ExampleColNum >= 0 && rowLength > _format.ExampleColNum && rowData[_format.ExampleColNum] != null  
            ? rowData[_format.ExampleColNum].Split(';', '|')
            : [];

        var transcription = _format.TranscriptionColNum >= 0 && rowLength > _format.TranscriptionColNum
            ? rowData[_format.TranscriptionColNum]
            : String.Empty;

        var firstMention = _format.FirstMentionColNum >= 0 && rowLength > _format.FirstMentionColNum
            ? rowData[_format.FirstMentionColNum]
            : String.Empty;

        var level = _format.LevelColNum >= 0 && rowLength > _format.LevelColNum
            ? rowData[_format.LevelColNum]
            : String.Empty;

        var definition = new List<string>();
        for (var j = _format.DefinitionWordsColNum; j < rowLength; j++)
        {
            if (!String.IsNullOrEmpty(rowData[j]))
                definition.Add(rowData[j]);
        }

        var sourceBuilder = rowData.Aggregate(new StringBuilder(), (s, x) => s.AppendLine(x));

        return new ImportedWord()
               {
                   Flags         = flags,
                   Class         = partOfSpeech,
                   ForeignWord   = foreignWord,
                   Definition    = definition.Where(x => !String.IsNullOrWhiteSpace(x)).ToList(),
                   Synonims      = synonims.Where(x => !String.IsNullOrWhiteSpace(x)).ToList(),
                   Transcription = transcription,
                   Examples      = example.Where(x => !String.IsNullOrWhiteSpace(x)).ToList(),
                   FirstMention  = firstMention,
                   SourceId      = row.Index + 1,
                   Level         = level,
                   SourceRowData = sourceBuilder?.ToString()
               };
    }

    public PartsOfSpeech ParsePartOfSpeech(string partofspeech)
    {
        return partofspeech switch
               {
                   "noun"       => PartsOfSpeech.Noun,
                   "verb"       => PartsOfSpeech.Verb,
                   "adj"        => PartsOfSpeech.Adjective,
                   "adv"        => PartsOfSpeech.Adverb,
                   "phrase"     => PartsOfSpeech.Phrase,
                   "idiom"      => PartsOfSpeech.Idiom,
                   "noun slang" => PartsOfSpeech.Slang,
                   "slang"      => PartsOfSpeech.Slang,
                   _            => PartsOfSpeech.Other
               };
    }
}