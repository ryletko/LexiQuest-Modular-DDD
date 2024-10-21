namespace LexiQuest.Framework.Module.Quartz;

public interface IQuartz 
{
    Task<IQuartz> Start();
    Task Stop();
}