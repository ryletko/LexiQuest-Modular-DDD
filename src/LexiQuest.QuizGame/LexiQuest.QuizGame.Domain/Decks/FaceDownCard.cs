using LexiQuest.Framework.Domain;

namespace LexiQuest.QuizGame.Domain.Decks;

public class FaceDownCardId(Guid value) : TypedIdValueBase(value);

public class FaceDownCard : Entity
{
    public FaceDownCardId Id { get; private init; }
    public FaceDownCardPuzzleInfo FaceDownCardPuzzleInfo { get; private init; }
    
    private FaceDownCard()
    {
    }

    internal static FaceDownCard CreateNew(FaceDownCardPuzzleInfo faceDownCardPuzzleInfo)
    {
        return new FaceDownCard()
               {
                   Id            = new FaceDownCardId(Guid.NewGuid()),
                   FaceDownCardPuzzleInfo    = faceDownCardPuzzleInfo,
               };
    }

}