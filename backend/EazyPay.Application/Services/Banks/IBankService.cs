using EazyPay.Application.Services.Banks.Models;
using EazyPay.Application.Shared.Dtos;

namespace EazyPay.Application.Services.Banks;

public interface IBankService
{
    public BaseResponse<BankResponse> ValidatePayment(BankRequest request);
    public BaseResponse<BankResponse> SettlePayment(BankRequest request);
}