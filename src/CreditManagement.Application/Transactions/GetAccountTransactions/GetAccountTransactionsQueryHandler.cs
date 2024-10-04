using CreditManagement.Application.Abstractions.Messaging;
using CreditManagement.Application.Dtos;
using CreditManagement.Domain.Primitives.Result;
using CreditManagement.Domain.Transactions;

namespace CreditManagement.Application.Transactions.GetAccountTransactions;

public class GetAccountTransactionsQueryHandler(ITransactionRepository transactionRepository): IQueryHandler<GetAccountTransactionsQuery,Result<IEnumerable<TransactionResponseDto>>>
{
    public async Task<Result<IEnumerable<TransactionResponseDto>>> Handle(GetAccountTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = await transactionRepository.GetAccountTransactionsByAccountIdAsync(request.AccountId);
        var lstAccountResponseDto = transactions.ConvertAll(transaction => new TransactionResponseDto(transaction.Id,transaction.Date,transaction.Description,transaction.Amount ,transaction.Category.ToString(),transaction.IsAnomalous));

        return Result.Success<IEnumerable<TransactionResponseDto>>(lstAccountResponseDto);
    }
}