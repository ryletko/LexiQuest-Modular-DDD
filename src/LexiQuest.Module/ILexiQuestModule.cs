namespace LexiQuest.Module;

// абстракция для модуля, то есть можно использовать любую другую вариацию модуля совместно с фреймворковском, не обязательно ту, которая
// им предоставляется
public interface ILexiQuestModule
{
    ILexiQuestModule Configure();
    Task<ILexiQuestModule> Start();
    Task Stop();
}