using CreditManagement.Application.Abstractions.Data;
using CreditManagement.Domain.Accounts;
using CreditManagement.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace CreditManagement.Persistence.Repositories;

public class TransactionRepository(IDbContext dbContext)
    : GenericRepository<Transaction>(dbContext), ITransactionRepository
{
    public async Task<IEnumerable<Transaction>> GetAccountTransactionsByMonthAndYearAsync(Guid accountId, int year, int month)
    {
        return await DbContext.Set<Transaction>()
            .Where(t => t.AccountId == accountId && t.Date.Year == year && t.Date.Month == month).ToListAsync();

    }
    
    public Task<List<Transaction>> GetAccountTransactionsByAccountIdAsync(Guid accountId)
    {
        return DbContext.Set<Transaction>()
            .Where(t => t.AccountId == accountId).ToListAsync();

    }
}