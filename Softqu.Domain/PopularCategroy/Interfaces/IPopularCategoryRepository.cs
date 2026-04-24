using Softqu.Domain.PopularCategory;

namespace Softqu.Domain.PopularCategroy.Interfaces
{
    public interface IPopularCategoryRepository
    {
        Task<List<PopularCategoryAggregate>> GetPopularCategoriesAsync();
        Task AddAsync(PopularCategoryAggregate popularCategory);
        Task<bool> ExistCategroyAsync(Guid id);
        Task<bool> ExistOrderAsync(int sortOrder);
        Task<List<PopularCategoryAggregate>> GetByIdsAsync(List<Guid> ids);
        Task<PopularCategoryAggregate> GetByIdAsync(Guid id);
        Task Remove(PopularCategoryAggregate popularCategory);
        Task SaveChangesAsync();
    }
}
