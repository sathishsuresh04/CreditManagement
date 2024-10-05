using CreditManagement.Application.Abstractions.Data;
using CreditManagement.Domain.Accounts;
using CreditManagement.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CreditManagement.Persistence.Repositories;

public sealed class AccountRepository(IDbContext dbContext)
    : GenericRepository<Account>(dbContext), IAccountRepository

{
    public Task<List<Account>> GetAllAccountsWithTransactionsAsync()
    {
        return DbContext.Set<Account>()
            .Include(a => a.Transactions)
            .ToListAsync();
    }


    public Task<List<Account>> GetAccountsAsync()
    {
        return DbContext.Set<Account>()
            .ToListAsync();
    }

    public Task<Account?> GetAccountByAccountNumberAsync(string accountNumber)
    {
        return DbContext.Set<Account>()
            .Include(a => a.Transactions)
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
    }

    public Task<bool> AnyAsync()
    {
        return base.AnyAsync(new AccountSpecification());
    }

    public Task<Account?> GetAccountByIdWithTransactionsAsync(Guid accountId)
    {
        return DbContext.Set<Account>()
            .Include(a => a.Transactions)
            .FirstOrDefaultAsync(a => a.Id == accountId);
    }
}