namespace CreditManagement.Persistence.Configurations;

/// <summary>
///     Represents a lookup value for an enumerated type.
/// </summary>
/// <typeparam name="T">The enumerated type.</typeparam>
public class EnumLookup<T>
    where T : Enum
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EnumLookup{T}" /> class.
    /// </summary>
    public EnumLookup()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="EnumLookup{T}" /> class with the specified value and description.
    /// </summary>
    /// <param name="value">The enumerated value.</param>
    /// <param name="description">The description of the value.</param>
    public EnumLookup(T value, string description)
    {
        Id = Convert.ToInt32(value);
        Value = value;
        Name = value.ToString();
        Description = description;
    }

    /// <summary>
    ///     Gets or sets the unique identifier of the enumerated value.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the enumerated value.
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    ///     Gets or sets the name of the enumerated value.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the description of the enumerated value.
    /// </summary>
    public string? Description { get; set; }
}