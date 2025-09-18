using EazyPay.Core.Repositories;
using EazyPay.Core.Entities;
using EazyPay.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EazyPay.Infrastructure.Persistence.Repositories;

public class BankRepository(DataContext dataContext) : BaseRepository<Bank>(dataContext),  IBankRepository
{
    private readonly DataContext _dataContext =  dataContext;
    public async Task<Bank?> GetByBankNameAsync(string name)
    {
        return await _dataContext.Banks
            .FirstOrDefaultAsync(b => b.Name == name);
    }
}