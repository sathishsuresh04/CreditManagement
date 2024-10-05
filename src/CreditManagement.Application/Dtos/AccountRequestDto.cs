namespace CreditManagement.Application.Dtos;

public record AccountRequestDto(
    string AccountNumber,
    string AccountHolder,
    decimal Balance,
    IEnumerable<TransactionRequestDto> Transactions);