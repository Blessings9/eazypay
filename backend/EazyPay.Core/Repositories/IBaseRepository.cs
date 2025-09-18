using EazyPay.Core.Entities;

namespace EazyPay.Core.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    public Task<List<T>> GetAllAsync<T>() where T : BaseEntity;
    public Task<T?> GetByIdAsync<T>(long id) where T : BaseEntity;
    public Task<T> AddAsync<T>(T entity) where T : BaseEntity;
    public Task<T> UpdateAsync<T>(T entity) where T : BaseEntity;
}