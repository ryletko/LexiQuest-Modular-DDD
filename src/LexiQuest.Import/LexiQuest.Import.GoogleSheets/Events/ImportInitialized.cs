using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.Import.GoogleSheets.Events;

public record ImportInitialized(Guid ImportSourceId,
                                string Url,
                                Language Language) : CommandBase;