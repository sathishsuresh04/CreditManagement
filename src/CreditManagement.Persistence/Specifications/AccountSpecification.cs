using System.Linq.Expressions;
using CreditManagement.Domain.Accounts;

namespace CreditManagement.Persistence.Specifications;

public class AccountSpecification: Specification<Account>
{
    internal override Expression<Func<Account, bool>> ToExpression()
    {
        return account => true;
    }
}

