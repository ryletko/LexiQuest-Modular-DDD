namespace LexiQuest.Framework.Module.Quartz;

public class DummyQuartz: IQuartz
{
    public async Task<IQuartz> Start()
    {
        return this;
    }

    public async Task Stop()
    {
    }
}