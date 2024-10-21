using LexiQuest.QuizGame.Domain.Decks;
using LexiQuest.QuizGame.Domain.FaceUpCards;
using LexiQuest.QuizGame.Domain.FaceUpCards.CardPicking;
using LexiQuest.QuizGame.Domain.GameStates;
using LexiQuest.QuizGame.Domain.Players;

namespace LexiQuest.QuizGame.Domain.Services;

//:: since we only add new new entity records without querying and updating any data
//:: we can orchestrate creating new game in domain service. It can't cause any transactional conflicts
//:: alternatively it could be done with domain events initiated by PuzzleSet 
public class GameCreator
{
    public GameCreatorOutput CreateNewGame(IReadOnlyList<FaceDownCardPuzzleInfo> puzzleInfos, PlayerId playerId)
    {
        var cardDeck = CardDeck.CreateNew(puzzleInfos, playerId);
        var game = GameState.CreateNewGame(cardDeck.Id, playerId);
        var cardPicker = new RandomLessSolvedCardPicker(cardDeck.Cards, []);
        var faceUpCard = FaceUpCard.CreateNew(cardPicker, game.GameId, playerId);
        return new GameCreatorOutput()
               {
                   NewCardDeck     = cardDeck,
                   NewGameState    = game,
                   FirstFaceUpCard = faceUpCard
               };
    }
}

public class GameCreatorOutput
{
    public required CardDeck NewCardDeck { get; init; }
    public required GameState NewGameState { get; init; }
    public required FaceUpCard FirstFaceUpCard { get; init; }
}
