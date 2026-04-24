using Microsoft.EntityFrameworkCore;
using Softqu.Domain.Category;
using Softqu.Domain.Category.Interfaces;
using Softqu.Infrastructure.Data;

namespace Softqu.Infrastucture.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;
        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<CategoryAggregate>> GetCategoriesTreeAsync()
        {
            return await context.Categories
                .AsNoTracking()
                .Where(c => c.ParentCategoryId == null && !c.IsDeleted)
                .Include(c => c.Translations)
                .Include(c => c.ChildCategories.Where(child => !child.IsDeleted))
                    .ThenInclude(cc => cc.Translations)
                .Include(c => c.ChildCategories)
                    .ThenInclude(cc => cc.ChildCategories.Where(gchild => !gchild.IsDeleted))
                    .ThenInclude(cc => cc.Translations)
                .OrderBy(c => c.Translations.FirstOrDefault(t => t.LanguageCode == "en").Title ?? c.Translations.FirstOrDefault().Title)
                .ToListAsync();
        }

        public async Task AddAsync(CategoryAggregate category)
        {
            await context.Categories.AddAsync(category);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> AnyWithSlugAsync(string slug)
        {
            return await context.Categories.AnyAsync(c => c.Slug == slug);
        }

        public async Task<CategoryAggregate?> GetByIdAsync(Guid id)
        {
            return await context.Categories
                .Include(c => c.Translations)
                .Include(c => c.ChildCategories)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<bool> AnyWithSlugExceptAsync(string slug, Guid id)
        {
            return await context.Categories.AnyAsync(c => c.Slug == slug && c.Id != id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
