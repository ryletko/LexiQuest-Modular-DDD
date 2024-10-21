using LexiQuest.QuizGame.Domain.Decks;

namespace LexiQuest.QuizGame.Domain.FaceUpCards.CardPicking;

public sealed class RandomLessSolvedCardPicker(IReadOnlyList<FaceDownCard> puzzles,
                                               IReadOnlyList<FaceUpCard> faceUpCardsInGame)
{
    public FaceUpCardPuzzleInfo GetNewPuzzle()
    {
        var learnedCardsPuzzleIdCount = faceUpCardsInGame.Where(x => !x.Mistaken)
                                                         .GroupBy(t => t.PuzzleInfo.FaceDownCardId)
                                                         .Select(x => new
                                                                      {
                                                                          PuzzleId = x.Key,
                                                                          Count    = x.Count()
                                                                      });
        return (from p in puzzles
                from t in learnedCardsPuzzleIdCount.Where(x => x.PuzzleId == p.Id).DefaultIfEmpty()
                orderby t?.Count ?? 0, Guid.NewGuid()
                select new FaceUpCardPuzzleInfo(p.Id,
                                                p.FaceDownCardPuzzleInfo.ForeignWord,
                                                p.FaceDownCardPuzzleInfo.PartsOfSpeech,
                                                p.FaceDownCardPuzzleInfo.Transcription,
                                                p.FaceDownCardPuzzleInfo.From,
                                                p.FaceDownCardPuzzleInfo.Language,
                                                p.FaceDownCardPuzzleInfo.Definitions.ToList(),
                                                p.FaceDownCardPuzzleInfo.Synonims.ToList(),
                                                p.FaceDownCardPuzzleInfo.Examples.ToList(),
                                                p.FaceDownCardPuzzleInfo.Level)).First();
    }
}