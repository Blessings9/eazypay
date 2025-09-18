namespace EazyPay.Application.Services.Banks.Models;

public class BankRequest
{
    public required string CardNumber { get; set; }
    public required int MonthOfExpiry { get; set; }
    public required int YearOfExpiry { get; set; }
    public required string Cvv { get; set; }
    public required string CurrencyCode { get; set; }
    public required decimal Amount { get; set; }
}