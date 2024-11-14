using LexiQuest.Framework.Application.Messages.Queries;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.Import.GoogleSheets.Contracts.Queries;

public record GetImportSourcesQuery(string ImporterId) : QueryBase<GetImportSourcesQueryResult>;

public record GetImportSourcesQueryResult(List<GetImportSourcesQueryResult.GetImportSourcesQueryResultItem> Items)
{
    public record GetImportSourcesQueryResultItem(Guid Id,
                                                  string Name,
                                                  string Url,
                                                  Language Language);
}