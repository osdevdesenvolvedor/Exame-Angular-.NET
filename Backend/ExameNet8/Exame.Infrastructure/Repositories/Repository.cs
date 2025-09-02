using Exame.Domain.Interfaces;
using Exame.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Exame.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ExameDbContext _ctx;
    protected readonly DbSet<T> _db;

    public Repository(ExameDbContext ctx)
    {
        _ctx = ctx;
        _db = ctx.Set<T>();
    }

    public IQueryable<T> Query() => _db.AsQueryable();

    public Task<T?> GetAsync(params object[] key) => _db.FindAsync(key).AsTask();

    public Task AddAsync(T entity) => _db.AddAsync(entity).AsTask();

    public void Update(T entity) => _db.Update(entity);

    public void Remove(T entity) => _db.Remove(entity);

    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _ctx.SaveChangesAsync(ct);
}
