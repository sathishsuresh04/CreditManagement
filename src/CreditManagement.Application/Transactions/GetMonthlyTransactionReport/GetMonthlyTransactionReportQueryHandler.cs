using CreditManagement.Application.Abstractions.Messaging;
using CreditManagement.Application.Dtos;
using CreditManagement.Domain.Primitives.Result;
using CreditManagement.Domain.Transactions;

namespace CreditManagement.Application.Transactions.GetMonthlyTransactionReport;

internal sealed class GetMonthlyTransactionReportQueryHandler(ITransactionRepository transactionRepository): IQueryHandler<GetMonthlyTransactionReportQuery,Result<MonthlyReportDto>>
{
    public async Task<Result<MonthlyReportDto>> Handle(GetMonthlyTransactionReportQuery request, CancellationToken cancellationToken)
    {
        var transactions=await transactionRepository.GetAccountTransactionsByMonthAndYearAsync(request.AccountId,request.Month,request.Year);
        var enumerable = transactions.ToList();
        var totalIncome = enumerable.Where(t => t is { Category: TransactionCategory.Income, IsAnomalous: false }).Sum(t => t.Amount);
        var totalExpenses = enumerable.Where(t => t is { Category: TransactionCategory.Expense, IsAnomalous: false }).Sum(t => t.Amount);

        var categoryTransactionCounts = enumerable.GroupBy(t => t.Category)
            .Select(g => new CategoryTransactionCount(g.Key.ToString(), g.Count())).ToList();

        var flaggedTransactions = enumerable.Where(t => t.IsAnomalous).ToList();
        var lstTransactionResponseDto = flaggedTransactions.Select(flaggedTransaction => new TransactionResponseDto(flaggedTransaction.Id, flaggedTransaction.Date, flaggedTransaction.Description, flaggedTransaction.Amount, flaggedTransaction.Category.ToString(), flaggedTransaction.IsAnomalous)).ToList();

        return Result.Success(new MonthlyReportDto(request.AccountId, totalIncome, totalExpenses,
            categoryTransactionCounts, lstTransactionResponseDto));


    }
}