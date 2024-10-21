using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.QuizGame.Contracts.Commands;

public record StartNewGameCommand() : CommandBase
{

    public string Name { get; set; } = "StartNewGameCommand"; // !!!Я НЕ ЕБУ ПОЧЕМУ, НО ПОЧЕМУ ТО КОММАНДЫ БЕЗ СВОЙСТВ НЕ РАБОТАЮТ!!!

}