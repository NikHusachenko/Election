using Election.Api.EntityFramework.Entities;

namespace Election.Api.EntityFramework.GenericRepository;

public interface IRepository<T> where T : EntityBase
{
    IQueryable<T> Entities { get; }
    Task<int> Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}