using System.Linq.Expressions;

namespace Exame.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> Query();
    Task<T?> GetAsync(params object[] key);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
