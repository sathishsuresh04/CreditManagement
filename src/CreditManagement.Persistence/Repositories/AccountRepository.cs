using CreditManagement.Application.Abstractions.Data;
using CreditManagement.Domain.Accounts;
using Microsoft.EntityFrameworkCore;

namespace CreditManagement.Persistence.Repositories;

public sealed class AccountRepository(IDbContext dbContext)
    : GenericRepository<Account>(dbContext), IAccountRepository

{
    public async Task<Account?> GetAccountByIdWithTransactionsAsync(Guid accountId)
    {
        return await dbContext.Set<Account>()
            .Include(a => a.Transactions)
            .FirstOrDefaultAsync(a => a.Id == accountId);
    }

    public Task<List<Account>> GetAllAccountsWithTransactionsAsync()
    {
        return  dbContext.Set<Account>()
            .Include(a => a.Transactions)
            .ToListAsync();
    }
    
}
