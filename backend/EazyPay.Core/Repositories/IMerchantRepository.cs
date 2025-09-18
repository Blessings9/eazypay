using EazyPay.Core.Entities;

namespace EazyPay.Core.Repositories;

public interface IMerchantRepository : IBaseRepository<Merchant>
{
    public Task<Merchant?> GetByMerchantIdAsync(string merchantId);
}