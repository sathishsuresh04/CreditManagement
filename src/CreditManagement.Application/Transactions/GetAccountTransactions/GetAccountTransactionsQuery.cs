using CreditManagement.Application.Abstractions.Messaging;
using CreditManagement.Application.Dtos;
using CreditManagement.Domain.Primitives.Result;

namespace CreditManagement.Application.Transactions.GetAccountTransactions;

public record GetAccountTransactionsQuery(Guid AccountId) : IQuery<Result<IEnumerable<TransactionResponseDto>>>;