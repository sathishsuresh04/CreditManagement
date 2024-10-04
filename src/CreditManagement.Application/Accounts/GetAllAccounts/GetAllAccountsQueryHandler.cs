using CreditManagement.Application.Abstractions.Messaging;
using CreditManagement.Application.Dtos;
using CreditManagement.Domain.Accounts;
using CreditManagement.Domain.Primitives.Result;

namespace CreditManagement.Application.Accounts.GetAllAccounts;

internal sealed class GetAllAccountsQueryHandler(IAccountRepository accountRepository): IQueryHandler<GetAllAccountsQuery,Result<IEnumerable<AccountResponseDto>>>
{
    public async Task<Result<IEnumerable<AccountResponseDto>>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await accountRepository.GetAccountsAsync();
        var lstAccountResponseDto = accounts.ConvertAll(account => new AccountResponseDto(account.Id, account.AccountNumber, account.AccountHolder, account.Balance));

        return Result.Success<IEnumerable<AccountResponseDto>>(lstAccountResponseDto);
}
}