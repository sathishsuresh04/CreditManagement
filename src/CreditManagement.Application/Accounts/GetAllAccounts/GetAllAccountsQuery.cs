using CreditManagement.Application.Abstractions.Messaging;
using CreditManagement.Application.Dtos;
using CreditManagement.Domain.Primitives.Result;

namespace CreditManagement.Application.Accounts.GetAllAccounts;

public record GetAllAccountsQuery : IQuery<Result<IEnumerable<AccountResponseDto>>>;