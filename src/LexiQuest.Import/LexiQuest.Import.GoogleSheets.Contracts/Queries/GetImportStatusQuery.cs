using LexiQuest.Framework.Application.Messages.Queries;

namespace LexiQuest.Import.GoogleSheets.Contracts.Queries;

public record GetImportStatusQuery : QueryBase<GetImportStatusQueryResult>
{
    public Guid ImportId { get; set; }
}

public record GetImportStatusQueryResult 
{
    public bool Found { get; set; }
    public string? Status { get; set; }
}