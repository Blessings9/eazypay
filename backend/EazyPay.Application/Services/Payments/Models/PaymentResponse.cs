namespace EazyPay.Application.Services.Payments.Models;

public class PaymentResponse
{
    public required string Date  { get; set; }
    public required string CardNumber { get; set; }
    public required string Status { get; set; }
    public required string Message { get; set; }
    public string? TransactionId { get; set; }
}