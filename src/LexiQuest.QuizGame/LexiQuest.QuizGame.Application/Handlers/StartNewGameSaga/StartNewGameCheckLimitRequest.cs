using LexiQuest.Framework.Application.Messages.Queries;

namespace LexiQuest.QuizGame.Application.Handlers.StartNewGameSaga;

public record StartNewGameCheckLimitRequest : QueryBase<StartNewGameCheckLimitRequestResult>
{
}

public record StartNewGameCheckLimitRequestResult(bool IsDuplicate);