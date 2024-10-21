using LexiQuest.Framework.Application.Messages.Events;

namespace LexiQuest.QuizGame.Contracts.Events;

public record NewCardOpenedEvent(Guid GameId, Guid FaceUpCardId): EventBase;