using CreditManagement.Application.Abstractions.Messaging;
using CreditManagement.Domain.Primitives.Result;

namespace CreditManagement.Application.Accounts.CreateAccountTransactions;

public record CreateAccountTransactionsCommand(string AccountDetails) : ICommand<Result>;