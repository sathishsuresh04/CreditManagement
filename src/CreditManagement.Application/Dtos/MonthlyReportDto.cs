namespace CreditManagement.Application.Dtos;

public record MonthlyReportDto(
    Guid AccountId,
    decimal TotalIncome,
    decimal TotalExpenses,
    List<CategoryTransactionCount> CategoryTransactionCounts,
    List<TransactionResponseDto> FlaggedTransactions);

public record CategoryTransactionCount(string Category, int Count);