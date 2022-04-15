namespace Groceries.Models.ViewModels;

public class PaginatedEnumerableViewModel<TEntity>
{
    public IEnumerable<TEntity> Entities { get; set; }

    public PaginationViewModel PaginationModel { get; set; }

    public PaginatedEnumerableViewModel(IEnumerable<TEntity> entities, PaginationViewModel paginationInfo)
    {
        Entities = entities;
        PaginationModel = paginationInfo;
    }
}