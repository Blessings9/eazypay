using EazyPay.Core.Entities;

namespace EazyPay.Core.Repositories;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    public Task<Payment?> GetByTransactionIdAsync(string transactionId);
}