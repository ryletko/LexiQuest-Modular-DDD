using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.QuizGame.Domain.Decks;
using LexiQuest.QuizGame.Domain.FaceUpCards;
using LexiQuest.QuizGame.Domain.GameStates;
using LexiQuest.QuizGame.Domain.Players;
using LexiQuest.QuizGame.Domain.Services;
using Utils.Core;

namespace LexiQuest.QuizGame.Application.Games.CreateNewGame;

internal class CreateNewGameCommandHandler(ICardDeckRepository cardDeckRepository,
                                           IGameStateRepository gameStateRepository,
                                           IFaceUpCardRepository faceUpCardRepository,
                                           IEventBus eventBus) : CommandHandlerBase<CreateNewGameCommand, CommandGuidResult>, IEventBusMessageHandler
{
    public override async Task<CommandGuidResult> Handle(CreateNewGameCommand query, CancellationToken cancellationToken = default)
    {
        var gameCreator = new GameCreator();

        var puzzles = query.Puzzles
                             .Select(x => new FaceDownCardPuzzleInfo(x.ForeignWord,
                                                         x.PartsOfSpeech.GetEnumDescription(),
                                                         x.Transcription,
                                                         x.From,
                                                         x.Language,
                                                         x.Definitions,
                                                         x.Synonims,
                                                         x.Examples,
                                                         x.Level))
                             .ToList();

        var createdEntities = gameCreator.CreateNewGame(puzzles, new PlayerId(query.MessageContext.UserId));

        await cardDeckRepository.AddAsync(createdEntities.NewCardDeck, cancellationToken);
        await gameStateRepository.AddAsync(createdEntities.NewGameState, cancellationToken);
        await faceUpCardRepository.AddAsync(createdEntities.FirstFaceUpCard, cancellationToken);

        await eventBus.SendEvent(new NewGameCreatedEvent(createdEntities.NewGameState.GameId.Value));
        
        return new CommandGuidResult(createdEntities.NewCardDeck.Id.Value);
    }
}