using EazyPay.Application.Services.Payments.Models;
using EazyPay.Application.Shared.Dtos;

namespace EazyPay.Application.Services.Payments;

public interface IPaymentService
{
    public Task<BaseResponse<PaymentResponse>> GetPayment(string transactionId);
    public Task<BaseResponse<PaymentResponse>> RequestPayment(PaymentRequest request);
}