namespace CreditManagement.Persistence.Common;

public static class EfConstants
{
    public const string UuidGenerator = "uuid-ossp";
    public const string UuidAlgorithm = "uuid_generate_v4()";
    public const string DateAlgorithm = "now()";
    public const string DateAlgorithmUtc = "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'";

    /// <summary>Column types used in database table definitions.</summary>
    public static class ColumnTypes
    {
        public const string DecimalEighteenTwo = "decimal(18,2)";
        public const string DecimalTenTwo = "decimal(10,2)";
        public const string TinyText = "varchar(15)";
        public const string ShortText = "varchar(25)";
        public const string NormalText = "varchar(50)";
        public const string MediumText = "varchar(100)";
        public const string LongText = "varchar(250)";
        public const string ExtraLongText = "varchar(500)";
        public const string UnlimitedText = "text";
    }

    /// <summary>Table names used in migrations history.</summary>
    public static class Table
    {
        public const string MigrationHistoryTable = "__efmigrationshistory";
    }

    /// <summary>Lengths of string columns.</summary>
    public static class Length
    {
        public const int Tiny = 15;
        public const int Short = 25;
        public const int Normal = 50;
        public const int Medium = 50;
        public const int Long = 250;
        public const int ExtraLong = 500;
    }
}
