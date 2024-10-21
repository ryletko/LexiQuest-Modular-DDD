using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.Import.GoogleSheets.Contracts.Events;

public record ImportStatusChangedEvent(Guid ImportId,
                                       string Status) : CommandBase;