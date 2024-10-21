using LexiQuest.Framework.Domain;
using LexiQuest.QuizGame.Domain.Decks;
using LexiQuest.QuizGame.Domain.GameStates;

namespace LexiQuest.QuizGame.Domain.FaceUpCards.Events;

public record FaceUpCardProcessed(GameId GameId,
                                  FaceDownCardId FaceDownCardId,
                                  FaceUpCardCheckResult CardCheckResult) : DomainEventBase
{
}