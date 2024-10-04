namespace CreditManagement.Domain.Transactions;


public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid transactionId);
    void Insert(Transaction transaction);
    void Update(Transaction transaction);
    void Remove(Transaction transaction);
    Task AddRangeAsync(IEnumerable<Transaction> transactions);
    Task<IEnumerable<Transaction>> GetAccountTransactionsByMonthAndYearAsync(Guid accountId,int year, int month);
    Task<List<Transaction>> GetAccountTransactionsByAccountIdAsync(Guid accountId);
}