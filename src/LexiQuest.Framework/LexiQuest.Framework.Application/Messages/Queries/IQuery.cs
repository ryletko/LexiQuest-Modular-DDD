
using LexiQuest.Framework.Application.Messages.Context;

namespace LexiQuest.Framework.Application.Messages.Queries;

public interface IQuery<out TResult>: IContextedMessage {}