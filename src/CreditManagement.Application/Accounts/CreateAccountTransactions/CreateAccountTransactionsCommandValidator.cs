using System.Text.Json;
using CreditManagement.Application.Dtos;
using FluentValidation;

namespace CreditManagement.Application.Accounts.CreateAccountTransactions;

public class CreateAccountTransactionsCommandValidator : AbstractValidator<CreateAccountTransactionsCommand>
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public CreateAccountTransactionsCommandValidator()
    {
        RuleFor(x => x.AccountDetails)
            .NotEmpty().WithMessage("Account details must be provided.")
            .Must(BeValidJson).WithMessage("Invalid JSON data.")
            .Must(ContainAccounts).WithMessage("JSON data does not contain valid accounts.")
            .Must(HaveValidAccounts).WithMessage("One or more accounts are invalid.");
    }

    private static bool BeValidJson(string accountDetails)
    {
        try
        {
            _ = JsonDocument.Parse(accountDetails);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    private bool ContainAccounts(string accountDetails)
    {
        try
        {
            var accounts = JsonSerializer.Deserialize<List<AccountRequestDto>>(accountDetails, _jsonSerializerOptions);
            return accounts is { Count: > 0 };
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool HaveValidAccounts(CreateAccountTransactionsCommand command, string accountDetails,
        ValidationContext<CreateAccountTransactionsCommand> context)
    {
        try
        {
            var accounts = JsonSerializer.Deserialize<List<AccountRequestDto>>(accountDetails, _jsonSerializerOptions);
            if (accounts == null || accounts.Count == 0) return false;

            var isValid = true;
            var accountValidator = new AccountDtoValidator();

            foreach (var result in accounts.Select(account => accountValidator.Validate(account))
                         .Where(result => !result.IsValid))
            {
                isValid = false;
                foreach (var error in result.Errors) context.AddFailure(error.PropertyName, error.ErrorMessage);
            }

            return isValid;
        }
        catch (Exception)
        {
            return false;
        }
    }
}