namespace Groceries.Repositories;

public interface IRepository<T>
{
    public IQueryable<T> Entities { get; }

    public void Add(T entity);

    public void Update(T entity);

    public void Remove(T entity);
}