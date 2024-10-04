namespace CreditManagement.Application.Dtos;

public record TransactionResponseDto(Guid Id,DateTime Date,string? Description,decimal Amount,string Category,bool IsFlagged );