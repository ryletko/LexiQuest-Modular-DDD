using LexiQuest.Framework.Domain;
using LexiQuest.QuizGame.Domain.GameStates;

namespace LexiQuest.QuizGame.Domain.FaceUpCards.Events;

public record FaceUpCardCompleted(GameId GameId,
                                  FaceUpCardCheckResult CardCheckResult) : DomainEventBase;