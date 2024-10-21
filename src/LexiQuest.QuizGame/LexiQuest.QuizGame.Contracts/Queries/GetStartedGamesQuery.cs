using LexiQuest.Framework.Application.Messages.Queries;

namespace LexiQuest.QuizGame.Contracts.Queries;

public record GetStartedGamesQuery : QueryBase<GetStartedGamesQueryResult>
{
}

public record GetStartedGamesQueryResult(List<GetStartedGamesQueryResult.GameItem> StartedGames)
{
    public record GameItem(Guid GameId);
}