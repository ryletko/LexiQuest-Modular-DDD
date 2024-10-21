namespace LexiQuest.PuzzleMgr.Contracts.Commands;

public class CreateCollectionCommand(string name)
{
    public string Name { get; } = name;
}