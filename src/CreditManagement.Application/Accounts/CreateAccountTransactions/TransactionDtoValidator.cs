using CreditManagement.Application.Dtos;
using FluentValidation;

namespace CreditManagement.Application.Accounts.CreateAccountTransactions;

public class TransactionDtoValidator : AbstractValidator<TransactionRequestDto>
{
    public TransactionDtoValidator()
    {
        RuleFor(transaction => transaction.Date)
            .Must(date => date == default || date <= DateTime.Now)
            .WithMessage("Date should not be a future date.");

        RuleFor(transaction => transaction.Amount)
            .GreaterThanOrEqualTo(0).WithMessage("Amount should not be negative.");
    }
}