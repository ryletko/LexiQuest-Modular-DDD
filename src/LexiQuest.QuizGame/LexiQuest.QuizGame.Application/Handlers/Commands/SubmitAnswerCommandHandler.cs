using LexiQuest.Framework.Application.Errors;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.QuizGame.Application.FaceUpCards.Access;
using LexiQuest.QuizGame.Contracts.Commands;
using LexiQuest.QuizGame.Domain.FaceUpCards;

namespace LexiQuest.QuizGame.Application.Handlers.Commands;

internal class SubmitAnswerCommandHandler(IFaceUpCardRepository faceUpCardRepository) : CommandHandlerBase<SubmitAnswerCommand>, IEventBusMessageHandler
{
    public override async Task Handle(SubmitAnswerCommand command, CancellationToken cancellationToken)
    {
        var card = await faceUpCardRepository.GetById(new FaceUpCardId(command.FaceUpCardId), cancellationToken);
        card.CheckAccess(command, PermissionAction.Edit);
        card.GuessAnswer(new FaceUpCardAnswer(command.Answer));
    }
}
