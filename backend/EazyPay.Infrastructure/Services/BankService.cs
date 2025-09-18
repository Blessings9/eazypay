using EazyPay.Application.Services.Banks;
using EazyPay.Application.Services.Banks.Models;
using EazyPay.Application.Shared.Dtos;
using EazyPay.Core.Repositories;
using EazyPay.Infrastructure.Utilities;

namespace EazyPay.Infrastructure.Services;

public class BankService() : IBankService
{
    public BaseResponse<BankResponse> ValidatePayment(BankRequest request)
    {
        var response = new BaseResponse<BankResponse>()
        {
            RequestId = Guid.NewGuid().ToString(),
            Timestamp = DateTime.Now,
            Code = 202,
            Message = "Card has been successfully validated.",
            Errors = [],
            Data = new BankResponse
            {
                CardNumber = request.CardNumber.Mask('X'),
                TransactionId = null
            }
        };

        try
        {
            if (request.CardNumber.Length != 16)
            {
                response.Code = 400;
                response.Message = "Invalid request. Please check the provided details.";
                response.Errors.Add(new { Field = nameof(request.CardNumber), Details = "Card Number must have 16 characters" });
                response.Data = null;
                
                return response; 
            }
            
            if (request.MonthOfExpiry <= 0 || request.YearOfExpiry <= 0)
            {
                response.Code = 400;
                response.Message = "Invalid request. Please check the provided details.";
                response.Errors.Add(new { Field = $"{nameof(request.MonthOfExpiry)}, {nameof(request.YearOfExpiry)}", Details = "Expiry Month and Expiry Year must be greater than 0" } );
                response.Data = null;
                
                return response;
            }
            
            var expiryDate = Dates.Create(request.MonthOfExpiry, request.YearOfExpiry);
            
            if (!expiryDate.IsFutureDate())
            {
                response.Code = 400;
                response.Message = "Invalid request. Please check the provided details";
                response.Errors.Add(new { Field = $"{nameof(request.MonthOfExpiry)}, {nameof(request.YearOfExpiry)}", Details = "Card Expiration Date must be in the future" } );
                response.Data = null;
                
                return response;
            }

            if (request.Cvv == string.Empty)
            {
                response.Code = 400;
                response.Message = "Invalid request. Please check the provided details";
                response.Errors.Add(new { Field = nameof(request.Cvv), Details = "Cvv is required" } );
                response.Data = null;
                
                return response;
            }

            if (request.CurrencyCode.Length != 3)
            {
                response.Code = 400;
                response.Message = "Invalid request. Please check the provided details";
                response.Errors.Add(new { Field = nameof(request.CurrencyCode), Details = "Currency code must have 3 characters." });
                response.Data = null;
                
                return response;
            }

            if (request.Amount <= 0)
            {
                response.Code = 400;
                response.Message = "Invalid request. Please check the provided details";
                response.Errors.Add(new { Field = nameof(request.Amount), Details = "Amount must be greater than 0" });
                response.Data = null;

                return response;
            }
            
        }
        catch (Exception error)
        {
            response.Code = 500;
            response.Message = "Service unavailable. Please try again another time.";
            response.Errors = [];
            response.Data = null;
            
            return response;
        }

        return response;
    }

    public BaseResponse<BankResponse> SettlePayment(BankRequest request)
    {
        var response = new BaseResponse<BankResponse>()
        {
            RequestId = Guid.NewGuid().ToString(),
            Timestamp = DateTime.Now,
            Code = 200,
            Message = "Payment successfully processed.",
            Errors = [],
            Data = new BankResponse
            {
                CardNumber = request.CardNumber.Mask('X'),
                TransactionId = Guid.NewGuid().ToString()
            }
        };

        try
        {
            var lastDigit = int.Parse(request.CardNumber.Last().ToString());

            if (lastDigit % 2 > 0)
            {
                response.Code = 422;
                response.Message = "Payment could not be processed by bank.";
                response.Data = null;

                return response;
            }
            
        }
        catch (Exception error)
        {
            response.Code = 500;
            response.Message = "Service unavailable. Please try again another time.";
            response.Errors = [];
            response.Data = null;
            
            return response;
        }
        
        return response;
    }
}