namespace CreditManagement.Application.Dtos;

public record TransactionRequestDto(DateTime Date,string Description,decimal Amount);