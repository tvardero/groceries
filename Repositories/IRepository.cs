namespace Groceries.Repositories;

public interface IRepository<T>
{
    public IQueryable<T> Entities { get; }

    public void Add(T product);

    public void Update(T product);

    public void Remove(T product);
}