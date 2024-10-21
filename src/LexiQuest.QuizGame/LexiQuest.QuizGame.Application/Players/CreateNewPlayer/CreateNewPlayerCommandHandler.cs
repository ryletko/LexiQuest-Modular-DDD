using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.QuizGame.Domain.Players;

namespace LexiQuest.QuizGame.Application.Players.CreateNewPlayer;

internal class CreateNewPlayerCommandHandler(IPlayerRepository playerRepository) : CommandHandlerBase<CreateNewPlayerCommand>, IInternalMessageHandler
{
    public override async Task Handle(CreateNewPlayerCommand command, CancellationToken cancellationToken)
    {
        var player = Player.CreateNew(new PlayerId(command.PlayerId));
        await playerRepository.AddAsync(player, cancellationToken);
    }
}