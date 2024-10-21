using LexiQuest.Framework.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LexiQuest.Framework.Infrastructure.DataAccess.Converters;

public class IdentityIdConverter<TIdValue>(ConverterMappingHints mappingHints = null) : ValueConverter<TIdValue, string>(id => id.Value, value => Create(value), mappingHints)
    where TIdValue : IdentityId
{
    private static TIdValue Create(string id) => Activator.CreateInstance(typeof(TIdValue), id) as TIdValue;
}