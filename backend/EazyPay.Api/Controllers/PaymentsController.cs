using EazyPay.Application.Services.Payments;
using EazyPay.Application.Services.Payments.Models;
using Microsoft.AspNetCore.Mvc;

namespace EazyPay.Api.Controllers;

[ApiController]
[Route("api/eazypay/payment")]
public class PaymentsController(IPaymentService paymentService) : Controller
{
    private readonly IPaymentService _paymentService = paymentService;
    
    [HttpGet]
    public async Task<IActionResult> GetPaymentByTransactionIdAsync([FromQuery] string transactionId)
    {
        var response = await _paymentService.GetPayment(transactionId);
        return StatusCode(response.Code, response);
    }

    [HttpPost]
    public async Task<IActionResult> RequestPayment([FromBody] PaymentRequest request)
    {
        var response = await _paymentService.RequestPayment(request);
        return StatusCode(response.Code, response);
    }
}