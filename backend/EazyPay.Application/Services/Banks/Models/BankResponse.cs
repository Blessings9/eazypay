namespace EazyPay.Application.Services.Banks.Models;

public class BankResponse
{
    public required string CardNumber { get; set; }
    public string? TransactionId { get; set; }
}