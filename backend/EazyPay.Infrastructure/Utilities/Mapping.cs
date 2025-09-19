using EazyPay.Application.Services.Banks.Models;
using EazyPay.Application.Services.Payments.Models;
using EazyPay.Core.Entities;

namespace EazyPay.Infrastructure.Utilities;

public static class Mapping
{
    public static Payment ToPayment(this PaymentRequest request)
    {
        return new Payment()
        {
            TimeOfPayment = DateTime.Now.ToString(),
            CardNumber = request.CardNumber,
            MonthOfExpiry = request.MonthOfExpiry,
            YearOfExpiry = request.YearOfExpiry,
            Cvv = request.Cvv,
            CurrencyCode = request.CurrencyCode,
            Amount = request.Amount,
            IsPaid = false,
            TransactionId = null,
            Message = "Payment has been requested but not paid yet."
        };
    }

    public static PaymentResponse ToPaymentResponse(this Payment payment)
    {
        return new PaymentResponse()
        {
            Date = payment.TimeOfPayment,
            CardNumber = payment.CardNumber,
            Status = payment.IsPaid ? "SUCCESS" : "FAILED",
            Message = payment.Message,
            TransactionId = payment.TransactionId,
        };
    }

    public static BankRequest ToBankRequest(this PaymentRequest request)
    {
        return new BankRequest()
        {
            CardNumber = request.CardNumber,
            MonthOfExpiry = request.MonthOfExpiry,
            YearOfExpiry = request.YearOfExpiry,
            Cvv = request.Cvv,
            CurrencyCode = request.CurrencyCode,
            Amount = request.Amount,
        };
    }
}