namespace CreditManagement.Domain.Accounts;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid accountId);
    void Insert(Account account);
    
    void Update(Account account);
    void Remove(Account account);
    Task<List<Account>> GetAllAccountsWithTransactionsAsync();
    
    Task<Account?> GetAccountByAccountNumberAsync(string accountNumber);
    Task AddRangeAsync(IEnumerable<Account> accounts);
    Task<bool> AnyAsync();
    
    Task<List<Account>> GetAccountsAsync();
    
}