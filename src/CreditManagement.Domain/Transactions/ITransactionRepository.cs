namespace CreditManagement.Domain.Transactions;

/// <summary>
/// Defines a repository interface for managing transactions in the credit management system.
/// </summary>
public interface ITransactionRepository
{
    /// <summary>
    /// Asynchronously retrieves a Transaction by its identifier.
    /// </summary>
    /// <param name="transactionId">The unique identifier of the transaction.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the transaction if found; otherwise, null.</returns>
    Task<Transaction?> GetByIdAsync(Guid transactionId);

    /// <summary>
    /// Inserts a new transaction into the repository.
    /// </summary>
    /// <param name="transaction">The transaction entity to be inserted.</param>
    void Insert(Transaction transaction);

    /// <summary>
    /// Updates an existing transaction in the repository.
    /// </summary>
    /// <param name="transaction">The transaction entity to be updated.</param>
    void Update(Transaction transaction);

    /// <summary>
    /// Removes a transaction from the repository.
    /// </summary>
    /// <param name="transaction">The transaction entity to be removed.</param>
    void Remove(Transaction transaction);

    /// <summary>
    /// Asynchronously adds a range of transactions to the repository.
    /// </summary>
    /// <param name="transactions">The collection of transactions to be added.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddRangeAsync(IEnumerable<Transaction> transactions);

    /// <summary>
    /// Asynchronously retrieves transactions for a specific account within a given month and year.
    /// </summary>
    /// <param name="accountId">The unique identifier of the account.</param>
    /// <param name="year">The year in which the transactions occurred.</param>
    /// <param name="month">The month in which the transactions occurred.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of transactions for the specified account, month, and year.</returns>
    Task<IEnumerable<Transaction>> GetAccountTransactionsByMonthAndYearAsync(Guid accountId, int year, int month);

    /// <summary>
    /// Asynchronously retrieves all transactions associated with a specific account identifier.
    /// </summary>
    /// <param name="accountId">The unique identifier of the account.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of transactions associated with the specified account.</returns>
    Task<List<Transaction>> GetAccountTransactionsByAccountIdAsync(Guid accountId);
}