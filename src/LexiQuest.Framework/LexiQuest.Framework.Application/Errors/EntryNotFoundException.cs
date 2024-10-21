namespace LexiQuest.Framework.Application.Errors;

public class EntryNotFoundException<T>(Guid entryId) : Exception($"{typeof(T).Name} with ID: {entryId} not found")
{
    public Guid EntryId { get; } = entryId;
}