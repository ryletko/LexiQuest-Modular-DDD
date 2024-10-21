using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.Import.GoogleSheets.Commands;

public record FetchDataFromGoogle(string Url,
                                  Language Language) : CommandBase;