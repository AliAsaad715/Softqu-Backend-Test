using Microsoft.EntityFrameworkCore;
using Softqu.Domain.Category;
using Softqu.Domain.PopularCategory;
using Softqu.Domain.PopularCategroy.Interfaces;
using Softqu.Infrastructure.Data;

namespace Softqu.Infrastucture.Repositories
{
    public class PopularCategoryRepository : IPopularCategoryRepository
    {
        private readonly ApplicationDbContext context;
        public PopularCategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<PopularCategoryAggregate>> GetPopularCategoriesAsync()
        {
            return await context.PopularCategories
                .AsNoTracking()
                .Include(pc => pc.Category)
                    .ThenInclude(c => c.Translations)
                .OrderBy(pc => pc.SortOrder)
                .ToListAsync();
        }

        public async Task AddAsync(PopularCategoryAggregate popularCategory)
        {
            await context.PopularCategories.AddAsync(popularCategory);
        }

        public async Task<bool> ExistCategroyAsync(Guid id)
        {
            return await context.PopularCategories.AnyAsync(c => c.CategoryId == id);
        }

        public async Task<bool> ExistOrderAsync(int sortOrder)
        {
            return await context.PopularCategories.AnyAsync(c => c.SortOrder == sortOrder);
        }

        public async Task<List<PopularCategoryAggregate>> GetByIdsAsync(List<Guid> ids)
        {
            return await context.PopularCategories
                .Where(pc => ids.Contains(pc.Id))
                .ToListAsync();
        }

        public async Task<PopularCategoryAggregate> GetByIdAsync(Guid id)
        {
            return await context.PopularCategories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Remove(PopularCategoryAggregate popularCategory)
        {
            context.PopularCategories.Remove(popularCategory);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
