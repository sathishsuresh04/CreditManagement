namespace CreditManagement.Domain.Accounts;

/// <summary>
/// Interface for account repository that provides methods for interacting with account data in the data store.
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// Retrieves an account by its unique identifier.
    /// </summary>
    /// <param name="accountId">The unique identifier of the account.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the account if found; otherwise, null.</returns>
    Task<Account?> GetByIdAsync(Guid accountId);

    /// <summary>
    /// Inserts a new account into the repository.
    /// </summary>
    /// <param name="account">The account to be inserted.</param>
    void Insert(Account account);

    /// <summary>
    /// Updates the specified account in the data store.
    /// </summary>
    /// <param name="account">The account to update.</param>
    void Update(Account account);

    /// <summary>
    /// Removes an existing account from the repository.
    /// </summary>
    /// <param name="account">The account to be removed.</param>
    void Remove(Account account);

    Task<List<Account>> GetAllAccountsWithTransactionsAsync();

    /// <summary>
    /// Retrieves an account by its unique account number.
    /// </summary>
    /// <param name="accountNumber">The unique account number of the account.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the account if found; otherwise, null.</returns>
    Task<Account?> GetAccountByAccountNumberAsync(string accountNumber);

    /// <summary>
    /// Adds a range of accounts to the repository asynchronously.
    /// </summary>
    /// <param name="accounts">The collection of accounts to be added.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddRangeAsync(IEnumerable<Account> accounts);

    /// <summary>
    /// Checks if any accounts exist in the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether any accounts exist.</returns>
    Task<bool> AnyAsync();

    /// <summary>
    /// Retrieves a list of all accounts.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of accounts.</returns>
    Task<List<Account>> GetAccountsAsync();
}