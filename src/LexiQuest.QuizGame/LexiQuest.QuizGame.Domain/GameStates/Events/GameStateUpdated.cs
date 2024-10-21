using LexiQuest.Framework.Domain;

namespace LexiQuest.QuizGame.Domain.GameStates.Events;

public record GameStateUpdated(GameId GameId, GameStatus GameStatus) : DomainEventBase;