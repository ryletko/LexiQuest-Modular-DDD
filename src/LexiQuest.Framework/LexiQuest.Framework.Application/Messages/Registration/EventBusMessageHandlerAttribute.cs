namespace LexiQuest.Framework.Application.Messages.Registration;

public class EventBusMessageHandlerAttribute: Attribute;

public interface IEventBusMessageHandler; // удобнее чем аттрибуты потому что можно вывести список все derived классов
