using LexiQuest.Shared.Puzzle;
using MassTransit;

namespace LexiQuest.Import.GoogleSheets.ImportSaga;

public class ImportSagaData: SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    
    public DateTimeOffset Timestamp { get; set; }
    public Guid ImportId { get; set; }
    public string ImporterId { get; set; }
    public Guid ImportSourceId { get; set; }
    public Language Language { get; set; }
    public string? Url { get; set; }
    
    public bool Initialized { get; set; }
    public bool Fetched { get; set; }
    public bool SavedInPuzzleMgr { get; set; }
    // public Uri? ResponseAddress { get; set; }
    // public Guid? RequestId { get; set; }
}
