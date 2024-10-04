using CreditManagement.Application.Dtos;
using FluentValidation;

namespace CreditManagement.Application.Accounts.CreateAccountTransactions;

public class AccountDtoValidator : AbstractValidator<AccountRequestDto>
{
    public AccountDtoValidator()
    {
        RuleFor(account => account.AccountNumber)
            .NotEmpty().WithMessage("Account number must be provided.");

        RuleFor(account => account.AccountHolder)
            .NotEmpty().WithMessage("Account holder must be provided.");

        RuleFor(account => account.Balance)
            .GreaterThan(0).WithMessage("Balance must be greater than zero.");

        RuleForEach(account => account.Transactions)
            .SetValidator(new TransactionDtoValidator());
    }
}