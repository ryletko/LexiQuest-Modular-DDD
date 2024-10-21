using System.Collections.Concurrent;
using LexiQuest.Framework.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LexiQuest.Framework.Infrastructure.DataAccess.Converters;

/// <summary>
/// Based on https://andrewlock.net/strongly-typed-ids-in-ef-core-using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-4/
/// </summary>
public class StronglyTypedIdValueConverterSelector(ValueConverterSelectorDependencies dependencies) : ValueConverterSelector(dependencies)
{
    private readonly ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo> _converters = new();

    public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type providerClrType = null)
    {
        var baseConverters = base.Select(modelClrType, providerClrType);
        foreach (var converter in baseConverters)
        {
            yield return converter;
        }

        var underlyingModelType = UnwrapNullableType(modelClrType);
        var underlyingProviderType = UnwrapNullableType(providerClrType);

        if (underlyingProviderType is null || underlyingProviderType == typeof(Guid))
        {
            var isTypedIdValue = typeof(TypedIdValueBase).IsAssignableFrom(underlyingModelType);
            if (isTypedIdValue)
            {
                var converterType = typeof(TypedIdValueConverter<>).MakeGenericType(underlyingModelType);

                yield return _converters
                   .GetOrAdd((underlyingModelType, typeof(Guid)),
                             _ =>
                             {
                                 return new ValueConverterInfo(modelClrType: modelClrType,
                                                               providerClrType: typeof(Guid),
                                                               factory: valueConverterInfo => (ValueConverter) Activator.CreateInstance(converterType, valueConverterInfo.MappingHints));
                             });
            }
        }
        
        if (underlyingProviderType is null || underlyingProviderType == typeof(string))
        {
            if (typeof(IdentityId).IsAssignableFrom(underlyingModelType))
            {
                var converterType = typeof(IdentityIdConverter<>).MakeGenericType(underlyingModelType);

                yield return _converters.GetOrAdd((underlyingModelType, typeof(string)),
                                                  _ =>
                                                  {
                                                      return new ValueConverterInfo(modelClrType: modelClrType,
                                                                                    providerClrType: typeof(string),
                                                                                    factory: valueConverterInfo => (ValueConverter) Activator.CreateInstance(converterType, valueConverterInfo.MappingHints));
                                                  });
            }
        }
    }

    private static Type UnwrapNullableType(Type type)
    {
        if (type is null)
        {
            return null;
        }

        return Nullable.GetUnderlyingType(type) ?? type;
    }
}