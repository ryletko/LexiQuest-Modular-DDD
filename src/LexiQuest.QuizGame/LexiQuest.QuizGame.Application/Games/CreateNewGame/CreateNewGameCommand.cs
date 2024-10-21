using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.QuizGame.Application.Games.CreateNewGame;

internal record CreateNewGameCommand(List<CreateNewGameCommand.PuzzleItem> Puzzles) : CommandBase<CommandGuidResult>
{
    public record PuzzleItem(Guid PuzzleId,
                             string ForeignWord,
                             Language Language,
                             PartsOfSpeech PartsOfSpeech,
                             List<string> Definitions,
                             List<string> Examples,
                             List<string> Synonims,
                             string? Transcription,
                             string? Level,
                             string? From);
    
}