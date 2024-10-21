namespace LexiQuest.Import.GoogleSheets.Services;

public class RowFormat
{
    /// <summary>
    /// Номер колонки транскрипции
    /// </summary>
    public int TranscriptionColNum { get; set; }

    /// <summary>
    /// Номер колонки синонимов
    /// </summary>
    public int SynonimColNum { get; set; }

    /// <summary>
    /// C какой колонки начинаются слова на русском
    /// </summary>
    public int DefinitionWordsColNum { get; set; }

    /// <summary>
    /// Пример
    /// </summary>
    public int ExampleColNum { get; set; }

    /// <summary>
    /// Где впервые встретилось
    /// </summary>
    public int FirstMentionColNum { get; set; }

    /// <summary>
    /// Какой уровень инглиша должен быть
    /// </summary>
    public int LevelColNum { get; set; }
    
    public static readonly RowFormat Default = new()
                                               {
                                                   DefinitionWordsColNum  = 8,
                                                   ExampleColNum       = 6,
                                                   SynonimColNum       = 4,
                                                   TranscriptionColNum = 3,
                                                   FirstMentionColNum  = 5,
                                                   LevelColNum         = 7,
                                               };
}