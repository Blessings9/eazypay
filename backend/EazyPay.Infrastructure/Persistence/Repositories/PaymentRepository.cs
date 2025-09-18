using EazyPay.Core.Entities;
using EazyPay.Core.Repositories;
using EazyPay.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EazyPay.Infrastructure.Persistence.Repositories;

public class PaymentRepository(DataContext dataContext) : BaseRepository<Payment>(dataContext), IPaymentRepository
{
    private readonly DataContext _dataContext =  dataContext;
    
    public async Task<Payment?> GetByTransactionIdAsync(string transactionId)
    {
        return await _dataContext.Payments
            .FirstOrDefaultAsync(p =>  p.TransactionId == transactionId);
    }
}