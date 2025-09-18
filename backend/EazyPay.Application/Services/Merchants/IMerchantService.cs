using EazyPay.Application.Services.Merchants.Dtos;
using EazyPay.Application.Shared.Dtos;

namespace EazyPay.Application.Services.Merchants;

public interface IMerchantService
{
    public Task<BaseResponse<List<MerchantResponse>>> GetMerchantsAsync();
    public Task<BaseResponse<MerchantResponse>> GetMerchantByIdAsync(string merchantId);
    public Task<BaseResponse<MerchantResponse>> CreateMerchantAsync(MerchantRequest request);
    public Task<BaseResponse<MerchantResponse>> UpdateMerchantAsync(MerchantRequest request);
    public Task<BaseResponse<MerchantResponse>> DeleteMerchantAsync(string merchantId);
}