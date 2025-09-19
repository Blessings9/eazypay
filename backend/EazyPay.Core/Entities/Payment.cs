namespace EazyPay.Core.Entities;

public class Payment : BaseEntity
{
    public required string TimeOfPayment { get; set; }
    public required string CardNumber { get; set; }
    public required int MonthOfExpiry { get; set; }
    public required int YearOfExpiry { get; set; }
    public required string Cvv { get; set; }
    public required string CurrencyCode { get; set; }
    public required decimal Amount { get; set; }
    public required bool IsPaid { get; set; }
    public string? TransactionId { get; set; }
    public required string Message { get; set; }
}