using LexiQuest.Framework.Domain;

namespace LexiQuest.QuizGame.Domain.GameStates.Events;

public record GameFinished(GameId GameId) : DomainEventBase;