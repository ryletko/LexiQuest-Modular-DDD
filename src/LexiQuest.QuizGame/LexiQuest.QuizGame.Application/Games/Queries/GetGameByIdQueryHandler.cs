using LexiQuest.Framework.Application.DataAccess;
using LexiQuest.Framework.Application.Messages.Queries;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.QuizGame.Application.FaceUpCards;
using LexiQuest.QuizGame.Application.Games.Access;
using LexiQuest.QuizGame.Contracts.Queries;
using LexiQuest.QuizGame.Domain.Decks;
using LexiQuest.QuizGame.Domain.FaceUpCards;
using LexiQuest.QuizGame.Domain.GameStates;
using Microsoft.EntityFrameworkCore;
using Utils.Core;

namespace LexiQuest.QuizGame.Application.Games.Queries;

internal class GetGameByIdQueryHandler(IQueryContext queryContext) : QueryHandlerBase<GetGameById, GetGameByIdResult>, IEventBusMessageHandler
{
    public override async Task<GetGameByIdResult> Handle(GetGameById query, CancellationToken cancellationToken = default)
    {
        var gameId = new GameId(query.GameId);
        var result = await (from g in queryContext.GetUserFilteredGames(query)
                            from ps in queryContext.Query<CardDeck>().Where(x => x.Id == g.CardDeckId)
                            where g.GameId == gameId
                            select new
                                   {
                                       Id = g.GameId,
                                       g.CreatedTimestamp,
                                       g.Status,
                                       g.Score,
                                       CardDeckId = g.CardDeckId,
                                       CurrentFaceUpCard = queryContext.Query<FaceUpCard>()
                                                                       .FirstOrDefault(x => x.GameId == g.GameId && x.CompletedAt == null),
                                       PreviousFaceUpCard = queryContext.Query<FaceUpCard>()
                                                                        .Where(x => x.GameId == g.GameId && x.CompletedAt != null)
                                                                        .OrderByDescending(x => x.CompletedAt)
                                                                        .FirstOrDefault(),
                                       CardDeckSize   = ps.Cards.Count,
                                       CompletedCards = ps.Cards.Count(p => queryContext.Query<FaceUpCard>().Any(x => x.GameId == g.GameId && p.Id == x.PuzzleInfo.FaceDownCardId && !x.Mistaken && x.CompletedAt != null))
                                   })
                          .AsNoTracking()
                          .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (result == null)
            return new GetGameByIdResult(false, null);

        var turnMap = (FaceUpCardPuzzleInfo p) => new GetGameByIdResult.Puzzle(p.FaceDownCardId.Value,
                                                                               p.ForeignWord,
                                                                               p.PartsOfSpeech,
                                                                               p.Transcription,
                                                                               p.From,
                                                                               p.Language,
                                                                               p.Definitions.ToList(),
                                                                               p.Synonims.ToList(),
                                                                               p.Examples.ToList(),
                                                                               p.Level);


        return new GetGameByIdResult(true,
                                     new GetGameByIdResult.GameState(
                                         result.Id.Value,
                                         result.CreatedTimestamp,
                                         result.Status.GetEnumDescription(),
                                         result.Score.IntVal,
                                         result.CardDeckId.Value,
                                         result.CurrentFaceUpCard.Map(t => new GetGameByIdResult.FaceUpCard(t.Id.Value, t.Hint, t.Mistaken, t.LastResult.ToExternalCheckResult())),
                                         result.CurrentFaceUpCard.PuzzleInfo.Map(turnMap),
                                         result.PreviousFaceUpCard?.Map(t => new GetGameByIdResult.FaceUpCard(t.Id.Value, t.Hint, t.Mistaken, t.LastResult.ToExternalCheckResult())),
                                         result.PreviousFaceUpCard?.PuzzleInfo.Map(turnMap),
                                         result.CardDeckSize,
                                         result.CompletedCards));
    }
}
