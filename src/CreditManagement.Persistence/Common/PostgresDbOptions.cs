namespace CreditManagement.Persistence.Common;

public class PostgresDbOptions
{
    /// <summary>
    ///     Gets or sets the connection string for the PostgreSQL database .
    /// </summary>
    public string ConnectionString { get; init; } = default!;

    public string? DefaultSchema { get; init; }

    /// <summary>
    ///     Gets or sets the migration assembly name.
    /// </summary>
    public string? MigrationAssembly { get; init; } = null!;

    /// <summary>
    ///     Gets or sets a value indicating whether to execute raw SQL statements.
    /// </summary>
    public bool ExecuteRawSql { get; init; }

    /// <summary>
    ///     Gets or sets the command timeout in seconds.
    /// </summary>
    public int CommandTimeoutInSeconds { get; init; } = 30;
}
