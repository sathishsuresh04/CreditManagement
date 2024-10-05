namespace CreditManagement.Application.Dtos;

public record AccountResponseDto(Guid Id, string AccountNumber, string AccountHolder, decimal Balance);