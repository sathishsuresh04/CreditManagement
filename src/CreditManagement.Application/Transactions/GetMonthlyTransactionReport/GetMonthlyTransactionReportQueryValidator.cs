using FluentValidation;

namespace CreditManagement.Application.Transactions.GetMonthlyTransactionReport;

public class GetMonthlyTransactionReportQueryValidator : AbstractValidator<GetMonthlyTransactionReportQuery>
{
    public GetMonthlyTransactionReportQueryValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("Account ID must be provided.");

        RuleFor(x => x.Year)
            .InclusiveBetween(2000, DateTime.UtcNow.Year).WithMessage("Year must be between 2000 until current year.");

        RuleFor(x => x.Month)
            .InclusiveBetween(1, 12).WithMessage("Month must be between 1 and 12.");
    }
}