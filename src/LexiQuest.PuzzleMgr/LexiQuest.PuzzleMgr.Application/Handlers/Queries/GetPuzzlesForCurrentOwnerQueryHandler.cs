using LexiQuest.Framework.Application.DataAccess;
using LexiQuest.Framework.Application.Messages.Queries;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.PuzzleMgr.Contracts.Queries;
using LexiQuest.PuzzleMgr.Domain.PuzzleOwners;
using LexiQuest.PuzzleMgr.Domain.Puzzles;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.PuzzleMgr.Application.Handlers.Queries;

internal class GetPuzzlesForCurrentOwnerHandler(IQueryContext queryContext)
    : QueryHandlerBase<GetPuzzlesForCurrentOwnerQuery, GetPuzzlesForCurrentOwnerQueryResult>, IEventBusMessageHandler
{
    public override async Task<GetPuzzlesForCurrentOwnerQueryResult> Handle(GetPuzzlesForCurrentOwnerQuery query, CancellationToken cancellationToken = default)
    {
        var userId = new PuzzleOwnerId(query.MessageContext.UserId);
        var puzzleItems = await queryContext.Query<Puzzle>()
                                             //.Where(x => x.PuzzleOwnerId == userId)
                                            .Select(x => new GetPuzzlesForCurrentOwnerQueryResult.PuzzleItem(
                                                        x.Id.Value,
                                                        x.ForeignWord.ToString(),
                                                        x.Language,
                                                        x.PartsOfSpeech,
                                                        x.Definitions.Select(d => d.Text).ToList(),
                                                        x.Examples.Select(e => e.Text).ToList(),
                                                        x.Synonims.Select(s => s.Text).ToList(),
                                                        x.Transcription.StrVal,
                                                        x.Level.TextRepresentation,
                                                        x.From
                                                    ))
                                            .AsNoTracking()
                                            .ToListAsync(cancellationToken);
        return new GetPuzzlesForCurrentOwnerQueryResult(puzzleItems);
    }
}