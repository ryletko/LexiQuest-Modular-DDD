namespace LexiQuest.Framework.Infrastructure.InternalProcessing.InternalCommands;

internal interface IInternalCommandsMapper
{
    string GetName(Type type);

    Type GetType(string name);
}