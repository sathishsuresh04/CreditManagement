using System.Text.Json;
using CreditManagement.Application.Abstractions.Data;
using CreditManagement.Application.Accounts.CreateAccountTransactions;
using CreditManagement.Application.Dtos;
using CreditManagement.Domain.Accounts;
using CreditManagement.Domain.Transactions;
using FluentAssertions;
using NSubstitute;

namespace CreditManagement.Application.Tests;

public class CreateAccountTransactions
{
    private readonly IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();
    private readonly ITransactionRepository _transactionRepository = Substitute.For<ITransactionRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    [Fact]
    public async Task CreateAccountTransactions_NewAccount_ShouldCreateAccountAndTransactions()
    {
        // Arrange
        var command = new CreateAccountTransactionsCommand(JsonSerializer.Serialize(new List<AccountRequestDto>
        {
            new("123456", "John Doe", 1000m, new List<TransactionRequestDto>
            {
                new(DateTime.Now, "Salary", 500m),
                new(DateTime.Now, "Groceries", 100m)
            })
        }));

        _accountRepository.GetAccountByAccountNumberAsync("123456").Returns((Account)null);

        var handler = new CreateAccountTransactionsCommandHandler(
            _accountRepository,
            _transactionRepository,
            _unitOfWork);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _accountRepository.Received(1).Insert(Arg.Any<Account>());
        _transactionRepository.Received(2).Insert(Arg.Any<Transaction>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}