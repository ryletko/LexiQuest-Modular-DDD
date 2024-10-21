using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.QuizGame.Contracts.Commands;

public record SubmitAnswerCommand(Guid FaceUpCardId, string Answer) : CommandBase;
