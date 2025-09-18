using EazyPay.Application.Services.Banks;
using EazyPay.Application.Services.Payments;
using EazyPay.Application.Services.Payments.Models;
using EazyPay.Application.Shared.Dtos;
using EazyPay.Core.Repositories;
using EazyPay.Infrastructure.Utilities;

namespace EazyPay.Infrastructure.Services;

public class PaymentService(IPaymentRepository paymentRepository, IBankService bankService) : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IBankService _bankService = bankService;

    public async Task<BaseResponse<PaymentResponse>> GetPayment(string transactionId)
    {
        var response = new BaseResponse<PaymentResponse>()
        {
            RequestId = Guid.NewGuid().ToString(),
            Timestamp = DateTime.Now,
            Code = 200,
            Message = "Payment successfully retrieved.",
            Errors = []
        };

        try
        {
            var payment = await _paymentRepository.GetByTransactionIdAsync(transactionId);

            if (payment == null)
            {
                response.Code = 404;
                response.Message = "Payment retrieval failed.";
                response.Errors.Add(new
                {
                    Field = transactionId,
                    Details = "No payment with the specified transactionId could be found."
                });
                response.Data = null;

                return response;
            }

            response.Data = new PaymentResponse()
            {
                Date = payment.TimeOfPayment,
                CardNumber = payment.CardNumber,
                Status = payment.IsPaid ? "SUCCESS" : "FAILED",
                Message = payment.Message,
                TransactionId = payment.TransactionId
            };
        }
        catch (Exception error)
        {
            response.Code = 500;
            response.Message = "Service unavailable. Please try again another time.";
            response.Errors = [];
            response.Data = null;
        }

        return response;
    }

    public async Task<BaseResponse<PaymentResponse>> RequestPayment(PaymentRequest request)
    {
        var response = new BaseResponse<PaymentResponse>()
        {
            RequestId = Guid.NewGuid().ToString(),
            Timestamp = DateTime.Now,
            Code = 200,
            Message = "Payment successfully processed.",
            Errors = []
        };

        try
        {
            var validation = _bankService.ValidatePayment(request.ToBankRequest());

            if (validation.Code != 202)
            {
                response.Code = validation.Code;
                response.Message = validation.Message;
                response.Errors = validation.Errors;
                response.Data = null;
                
                return response;
            }

            request.CardNumber = validation.Data?.CardNumber ?? request.CardNumber;
            
            var createdPayment = await _paymentRepository.AddAsync(request.ToPayment());

            var settlement = _bankService.SettlePayment(request.ToBankRequest());
            
            createdPayment.IsPaid = settlement.Code == 200 ? true : false;
            createdPayment.Message = settlement.Message;
            createdPayment.TransactionId = settlement.Data?.TransactionId;
                
            var updatedPayment =  await _paymentRepository.UpdateAsync(createdPayment);
            
            if (settlement.Code != 200)
            {
                response.Code = settlement.Code;
                response.Message = settlement.Message;
                response.Errors = settlement.Errors;
                response.Data = updatedPayment.ToPaymentResponse();

                return response;
            }

            response.Data = updatedPayment.ToPaymentResponse();
        }
        catch (Exception error)
        {
            response.Code = 500;
            response.Message = "Service unavailable. Please try again another time.";
            response.Errors = [];
            response.Data = null;
        }

        return response;
    }
}