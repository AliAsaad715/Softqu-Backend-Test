namespace Softqu.Domain.Category.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryAggregate>> GetCategoriesTreeAsync();
        Task<CategoryAggregate?> GetByIdAsync(Guid id);
        Task AddAsync(CategoryAggregate category);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> AnyWithSlugAsync(string slug);
        Task<bool> AnyWithSlugExceptAsync(string slug, Guid id);
        Task SaveChangesAsync();
    }
}
