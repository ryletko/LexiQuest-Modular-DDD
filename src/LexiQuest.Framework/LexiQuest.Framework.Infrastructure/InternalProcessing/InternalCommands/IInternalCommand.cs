using MassTransit.Mediator;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.InternalCommands;

public interface IInternalCommand;

public interface IInternalCommand<out TR> : Request<TR> where TR : class;