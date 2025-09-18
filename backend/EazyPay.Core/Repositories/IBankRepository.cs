using EazyPay.Core.Entities;

namespace EazyPay.Core.Repositories;

public interface IBankRepository : IBaseRepository<Bank>
{
    public Task<Bank?> GetByBankNameAsync(string name);
}