using Microsoft.EntityFrameworkCore;
using Softqu.Domain.PopularCategory;
using Softqu.Domain.SwiperSlide;
using Softqu.Domain.SwiperSlide.Interfaces;
using Softqu.Infrastructure.Data;

namespace Softqu.Infrastucture.Repositories
{
    public class SwiperSlideRepository : ISwiperSlideRepository
    {
        private readonly ApplicationDbContext context;
        public SwiperSlideRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<SwiperSlideAggregate>> GetSwiperSlidesAsync()
        {
            return await context.SwiperSlides
                .AsNoTracking()
                .Include(pc => pc.Translations)
                .OrderBy(pc => pc.SortOrder)
                .ToListAsync();
        }

        public async Task AddAsync(SwiperSlideAggregate swiperSlide)
        {
            await context.SwiperSlides.AddAsync(swiperSlide);
        }

        public async Task<List<SwiperSlideAggregate>> GetByIdsAsync(List<Guid> ids)
        {
            return await context.SwiperSlides
                .Where(pc => ids.Contains(pc.Id))
                .ToListAsync();
        }

        public async Task<List<SwiperSlideAggregate>> GetSwiperSlidesBySortOrderAsync(int sortOrder)
        {
            return await context.SwiperSlides
                .Where(x => x.SortOrder >= sortOrder)
                .ToListAsync();
        }

        public async Task<SwiperSlideAggregate?> GetByIdAsync(Guid id)
        {
            return await context.SwiperSlides
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
