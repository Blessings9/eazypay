using EazyPay.Application.Shared.Dtos;
using EazyPay.Application.Shared.Models;

namespace EazyPay.Application.Services.Payments.Models;

public class PaymentRequest     
{
    public required string CardNumber { get; set; }
    public required int MonthOfExpiry { get; set; }
    public required int YearOfExpiry { get; set; }
    public required string Cvv { get; set; }
    public required string CurrencyCode { get; set; }
    public required decimal Amount { get; set; }
}