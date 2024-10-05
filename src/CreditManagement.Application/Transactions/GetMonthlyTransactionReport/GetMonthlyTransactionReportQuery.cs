using CreditManagement.Application.Abstractions.Messaging;
using CreditManagement.Application.Dtos;
using CreditManagement.Domain.Primitives.Result;

namespace CreditManagement.Application.Transactions.GetMonthlyTransactionReport;

public record GetMonthlyTransactionReportQuery(Guid AccountId, int Year, int Month) : IQuery<Result<MonthlyReportDto>>;