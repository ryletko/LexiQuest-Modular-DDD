using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.Import.GoogleSheets.Commands;

public record InitializeImport(Guid ImportSourceId) : CommandBase;