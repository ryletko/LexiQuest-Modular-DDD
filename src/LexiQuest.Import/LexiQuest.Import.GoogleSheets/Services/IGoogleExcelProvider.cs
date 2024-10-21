namespace LexiQuest.Import.GoogleSheets.Services;

internal interface IGoogleExcelProvider
{
    Task<Stream> GetGoogleExcel(string url, CancellationToken cancellationToken);
}
