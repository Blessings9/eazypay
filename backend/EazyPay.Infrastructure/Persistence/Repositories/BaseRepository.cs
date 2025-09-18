using EazyPay.Core.Entities;
using EazyPay.Core.Repositories;
using EazyPay.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EazyPay.Infrastructure.Persistence.Repositories;

public class BaseRepository<T>(DataContext dataContext) : IBaseRepository<T> where T : BaseEntity
{
    private readonly DataContext _dataContext = dataContext;
    
    public async Task<List<T>> GetAllAsync<T>() where T : BaseEntity
    {
        return await _dataContext.Set<T>()
            .OrderByDescending(t => t.Id )
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync<T>(long id) where T : BaseEntity
    {
        return await _dataContext.Set<T>()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
    {
        var createdEntity = await _dataContext.Set<T>().AddAsync(entity);
        await  _dataContext.SaveChangesAsync();
        
        return createdEntity.Entity;
    }

    public async Task<T> UpdateAsync<T>(T entity) where T : BaseEntity
    {
        var updatedEntity = _dataContext.Set<T>().Update(entity);
        await _dataContext.SaveChangesAsync();
        
        return updatedEntity.Entity;
    }
}