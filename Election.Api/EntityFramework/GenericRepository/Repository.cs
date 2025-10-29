using Election.Api.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Election.Api.EntityFramework.GenericRepository;

public sealed class Repository<T>(AppDbContext context) : IRepository<T> where T : EntityBase
{
    private DbSet<T> Table => context.Set<T>();

    public IQueryable<T> Entities => Table.AsNoTracking();

    public async Task<int> Add(T entity)
    {
        await Table.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public Task Delete(T entity)
    {
        Table.Remove(entity);
        return context.SaveChangesAsync();
    }

    public Task Update(T entity)
    {
        Table.Update(entity);
        return context.SaveChangesAsync();
    }
}