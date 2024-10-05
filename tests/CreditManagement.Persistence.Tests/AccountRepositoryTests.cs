using System.Linq.Expressions;
using CreditManagement.Application.Abstractions.Data;
using CreditManagement.Domain.Accounts;
using CreditManagement.Persistence.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NSubstitute;

namespace CreditManagement.Persistence.Tests;

public class AccountRepositoryTests
{
    private readonly AccountRepository _accountRepository;
    private readonly IDbContext _dbContext;

    public AccountRepositoryTests()
    {
        _dbContext = Substitute.For<IDbContext>();
        _accountRepository = new AccountRepository(_dbContext);
    }

    [Fact]
    public async Task GetAccountByAccountNumberAsync_ShouldReturnCorrectAccount()
    {
        // Arrange
        var accountNumber = "123456";
        var account = Account.Create(accountNumber, "John Doe", 1000m);

        var mockSet = MockDbSet([account]);
        _dbContext.Set<Account>().Returns(mockSet);

        // Act
        var result = await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);

        // Assert
        result.Should().NotBeNull();
        result!.AccountNumber.Should().Be(accountNumber);
    }

    private static DbSet<T> MockDbSet<T>(List<T> list) where T : class
    {
        var queryable = list.AsQueryable();
        var mockSet = Substitute.For<DbSet<T>, IQueryable<T>, IAsyncEnumerable<T>>();

        mockSet.As<IQueryable<T>>().Provider.Returns(new TestAsyncQueryProvider<T>(queryable.Provider));
        mockSet.As<IQueryable<T>>().Expression.Returns(queryable.Expression);
        mockSet.As<IQueryable<T>>().ElementType.Returns(queryable.ElementType);
        using var enumerator = mockSet.As<IQueryable<T>>().GetEnumerator();
        using var returnThis = queryable.GetEnumerator();
        enumerator.Returns(returnThis);
        mockSet.As<IAsyncEnumerable<T>>().GetAsyncEnumerator()
            .Returns(_ => new TestAsyncEnumerator<T>(queryable.GetEnumerator()));

        return mockSet;
    }
}

internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;

    internal TestAsyncQueryProvider(IQueryProvider inner)
    {
        _inner = inner;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        return new TestAsyncEnumerable<TEntity>(expression);
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        return new TestAsyncEnumerable<TElement>(expression);
    }

    public object? Execute(Expression expression)
    {
        return _inner.Execute(expression);
    }

    public TResult Execute<TResult>(Expression expression)
    {
        return _inner.Execute<TResult>(expression);
    }

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
    {
        var expectedResultType = typeof(TResult).GetGenericArguments()[0];
        var executionResult = typeof(IQueryProvider)
            .GetMethod(
                nameof(IQueryProvider.Execute),
                1,
                [typeof(Expression)])
            ?.MakeGenericMethod(expectedResultType)
            .Invoke(this, [expression]);

        return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
            ?.MakeGenericMethod(expectedResultType)
            .Invoke(null, [executionResult])!;
    }
}

internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
    public TestAsyncEnumerable(IEnumerable<T> enumerable)
        : base(enumerable)
    {
    }

    public TestAsyncEnumerable(Expression expression)
        : base(expression)
    {
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }

    IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
}

internal class TestAsyncEnumerator<T>(IEnumerator<T> inner) : IAsyncEnumerator<T>
{
    public T Current => inner.Current;

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(inner.MoveNext());
    }

    public ValueTask DisposeAsync()
    {
        inner.Dispose();
        return new ValueTask();
    }
}