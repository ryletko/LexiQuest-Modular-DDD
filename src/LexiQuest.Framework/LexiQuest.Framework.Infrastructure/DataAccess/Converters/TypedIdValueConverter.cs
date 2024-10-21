using LexiQuest.Framework.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LexiQuest.Framework.Infrastructure.DataAccess.Converters;

public class TypedIdValueConverter<TTypedIdValue>(ConverterMappingHints mappingHints = null) : ValueConverter<TTypedIdValue, Guid>(id => id.Value, value => Create(value), mappingHints)
    where TTypedIdValue : TypedIdValueBase
{
    private static TTypedIdValue Create(Guid id) => Activator.CreateInstance(typeof(TTypedIdValue), id) as TTypedIdValue;
}