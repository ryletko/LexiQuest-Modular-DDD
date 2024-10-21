using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.Import.GoogleSheets.Commands;
using LexiQuest.Import.GoogleSheets.Events;
using LexiQuest.Import.GoogleSheets.Services;

namespace LexiQuest.Import.GoogleSheets.Handlers;

internal class FetchDataFromGoogleHandler(GoogleXlsxImportService googleXlsxImportService,
                                          IEventBus eventBus) : CommandHandlerBase<FetchDataFromGoogle>
{
    public override async Task Handle(FetchDataFromGoogle command, CancellationToken cancellationToken)
    {
        var importedWords = await googleXlsxImportService.ImportAsync(command.Url, cancellationToken).ToListAsync(cancellationToken: cancellationToken);
        var fetchedPuzzleItem = importedWords.Select(x => new ImportedDataFetched.FetchedPuzzleItem(x.ForeignWord,
                                                                                                    x.Class,
                                                                                                    x.Transcription,
                                                                                                    x.FirstMention,
                                                                                                    command.Language,
                                                                                                    x.Definition,
                                                                                                    x.Synonims,
                                                                                                    x.Examples,
                                                                                                    x.Level)).ToList();

        // здесь нужно выгрузить из google sheet во внутреннюю БД
        // нужно сохранить в БД для того чтобы в случае чего восстановиться и продолжить импорт
        await eventBus.SendEvent(new ImportedDataFetched(fetchedPuzzleItem));
    }
}