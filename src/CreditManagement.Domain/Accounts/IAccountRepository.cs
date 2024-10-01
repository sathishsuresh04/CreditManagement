namespace CreditManagement.Domain.Accounts;

public interface IAccountRepository
{
    Task<Account?> GetAccountByIdWithTransactionsAsync(Guid accountId);
    Task<List<Account>> GetAllAccountsWithTransactionsAsync();
}