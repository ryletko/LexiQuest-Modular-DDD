using FluentValidation;
using LexiQuest.Framework.Application.Errors;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Decoration;
using MassTransit;

namespace LexiQuest.Framework.Infrastructure.EventBus.Decorators;

public class ValidationCommandHandlerDecorator<T>(IList<IValidator<T>> validators) : IHandlerDecorator<T> where T : class, ICommand
{
    public async Task Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T> iterator)
    {
        var errors = validators
                    .Select(v => v.Validate(context.Message))
                    .SelectMany(result => result.Errors)
                    .Where(error => error != null)
                    .ToList();

        if (errors.Any())
        {
            throw new InvalidCommandException(errors.Select(x => x.ErrorMessage).ToList());
        }

        await iterator.Next();
    }
}

public class ValidationCommandHandlerDecorator<T, TR>(IList<IValidator<T>> validators) : IHandlerDecorator<T, TR>
    where T : class, ICommand<TR>
    where TR : class
{
    
    public async Task<TR> Consume(ConsumeContext<T> context, HandlerDecoratorIterator<T, TR> iterator)
    {
        var errors = validators
                    .Select(v => v.Validate(context.Message))
                    .SelectMany(result => result.Errors)
                    .Where(error => error != null)
                    .ToList();

        if (errors.Any())
        {
            throw new InvalidCommandException(errors.Select(x => x.ErrorMessage).ToList());
        }

        return await iterator.Next();
    }
}