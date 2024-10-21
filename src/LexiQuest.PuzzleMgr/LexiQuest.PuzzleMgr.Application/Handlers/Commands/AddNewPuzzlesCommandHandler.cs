using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.PuzzleMgr.Contracts.Commands;
using LexiQuest.PuzzleMgr.Contracts.Events;
using LexiQuest.PuzzleMgr.Domain.PuzzleOwners;
using LexiQuest.PuzzleMgr.Domain.Puzzles;
using LexiQuest.PuzzleMgr.Domain.Puzzles.ForeignWords;
using LexiQuest.Shared.Puzzle;
using Utils.Core;

namespace LexiQuest.PuzzleMgr.Application.Handlers.Commands;

internal class AddNewPuzzlesCommandHandler(IPuzzleRepository puzzleRepository,
                                           ILanguageLevelRepository languageLevelRepository,
                                           IEventBus eventBus) : CommandHandlerBase<AddNewPuzzlesCommand>, IEventBusMessageHandler
{
    public override async Task Handle(AddNewPuzzlesCommand command, CancellationToken cancellationToken)
    {
        var languageLevels = new Dictionary<(Language, string), LanguageLevel>();
        var puzzlesToAdd = await command.PuzzleItems.ToAsyncEnumerable()
                                        .SelectAwait(async x =>
                                                     {
                                                         var language = x.Language;
                                                         var langLevel = await x.Level.MapIfNotNullAsync(l => languageLevels.GetOrAdd((language, l), () => languageLevelRepository.GetByName(language, x.Level, cancellationToken)));
                                                         return Puzzle.CreateNew(new PuzzleOwnerId(command.MessageContext.UserId),
                                                                                 language,
                                                                                 ForeignWord.FromString(x.ForeignWord, language),
                                                                                 x.PartsOfSpeech,
                                                                                 x.Transcription?.Map(Transcription.FromString),
                                                                                 x.From,
                                                                                 x.Definitions.Select(Definition.FromString).ToList(),
                                                                                 x.Examples.Select(Example.FromString).ToList(),
                                                                                 x.Synonims.Select(s => ForeignWord.FromString(s, language)).ToList(),
                                                                                 langLevel);
                                                     })
                                        .ToListAsync(cancellationToken);

        await puzzleRepository.AddAsync(puzzlesToAdd, cancellationToken);
        await eventBus.SendEvent(new NewPuzzlesAdded());
    }
}