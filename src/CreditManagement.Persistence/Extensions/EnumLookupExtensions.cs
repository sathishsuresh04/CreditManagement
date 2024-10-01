using System.ComponentModel;
using System.Reflection;
using CreditManagement.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CreditManagement.Persistence.Extensions;

public static class EnumLookupExtensions
{
    /// <summary>
    ///     Creates Enum tables automatically by scanning the assembly entities.
    /// </summary>
    /// <param name="modelBuilder">The ModelBuilder instance.</param>
    /// <param name="createForeignKeys">Indicates whether to create foreign keys.</param>
    /// <exception cref="InvalidOperationException">Thrown when the operation is not valid for the object's current state.</exception>
    public static void CreateEnumLookupTable(this ModelBuilder modelBuilder, bool createForeignKeys = false)
    {
        var source = new List<Type>();
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).ToArray())
        {
            var entityType = property.DeclaringEntityType;
            var propertyType = property.ClrType;
            if (!propertyType.IsEnum) continue;


            var concreteType = typeof(EnumLookup<>).MakeGenericType(propertyType);
            var enumLookupBuilder = modelBuilder.Entity(concreteType);
            enumLookupBuilder.HasAlternateKey(nameof(EnumLookup<Enum>.Value));

            var data = Enum.GetValues(propertyType)
                .Cast<object>()
                .Select(
                    v =>
                    {
                        var name = Enum.GetName(propertyType, v);
                        var descriptionAttribute = propertyType
                            .GetField(name ?? throw new InvalidOperationException("description is empty"))
                            ?.GetCustomAttribute<DescriptionAttribute>();
                        var description = descriptionAttribute?.Description;
                        return Activator.CreateInstance(concreteType, v, description);
                    })
                .ToArray();

            if (!source.Any((Func<Type, bool>)(t => t == propertyType)))
            {
                enumLookupBuilder.HasData(data);
                source.Add(propertyType);
            }

            if (createForeignKeys)
            {
                modelBuilder.Entity(entityType.Name)
                    .HasOne(concreteType)
                    .WithMany()
                    .HasPrincipalKey(nameof(EnumLookup<Enum>.Value))
                    .HasForeignKey(property.Name);
            }
        }
    }
}