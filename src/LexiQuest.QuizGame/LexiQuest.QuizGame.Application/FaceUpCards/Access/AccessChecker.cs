using LexiQuest.Framework.Application.Errors;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.QuizGame.Domain.FaceUpCards;

namespace LexiQuest.QuizGame.Application.FaceUpCards.Access;

public static class AccessChecker
{
    public static void CheckAccess(this FaceUpCard faceUpCard, ICommand command, PermissionAction action)
    {
        if (faceUpCard.PlayerId.Value != command.MessageContext.UserId)
            throw new AccessDeniedException(command.MessageContext.UserId, $"face-up card {faceUpCard.GameId}", PermissionAction.Edit);
    }
}