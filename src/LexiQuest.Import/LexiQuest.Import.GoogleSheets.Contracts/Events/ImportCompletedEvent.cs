using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.Import.GoogleSheets.Contracts.Events;

public record ImportCompletedEvent(Guid ImportId): CommandBase;