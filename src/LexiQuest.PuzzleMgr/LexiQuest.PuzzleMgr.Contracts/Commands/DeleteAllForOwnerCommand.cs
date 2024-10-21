using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.PuzzleMgr.Contracts.Commands;

public record DeleteAllForOwnerCommand: CommandBase
{
    public bool Dummy { get; set; }
}