using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.Import.GoogleSheets.Contracts.Commands;

public record ImportCommand(Guid ImportSourceId): CommandBase<CommandGuidResult>;