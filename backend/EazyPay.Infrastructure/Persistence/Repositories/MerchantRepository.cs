using EazyPay.Core.Entities;
using EazyPay.Core.Repositories;
using EazyPay.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EazyPay.Infrastructure.Persistence.Repositories;

public class MerchantRepository(DataContext dataContext) :  BaseRepository<Merchant>(dataContext), IMerchantRepository
{
    private readonly DataContext _dataContext = dataContext;
    
    public async Task<Merchant?> GetByMerchantIdAsync(string merchantId)
    {
        return await _dataContext.Merchants
            .FirstOrDefaultAsync(m => m.MerchantId == merchantId);
    }
}