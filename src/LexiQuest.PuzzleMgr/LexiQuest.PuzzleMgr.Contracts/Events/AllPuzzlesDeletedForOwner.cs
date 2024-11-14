using LexiQuest.Framework.Application.Messages.Events;

namespace LexiQuest.PuzzleMgr.Contracts.Events;

public record AllPuzzlesDeletedForOwner(string OwnerId) : EventBase;