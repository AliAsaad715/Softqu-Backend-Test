namespace Softqu.Domain.SwiperSlide.Interfaces
{
    public interface ISwiperSlideRepository
    {
        Task<List<SwiperSlideAggregate>> GetSwiperSlidesAsync();
        Task AddAsync(SwiperSlideAggregate swiperSlide);
        Task<SwiperSlideAggregate?> GetByIdAsync(Guid id);
        Task<List<SwiperSlideAggregate>> GetSwiperSlidesBySortOrderAsync(int sortOrder);
        Task<List<SwiperSlideAggregate>> GetByIdsAsync(List<Guid> ids);
        Task SaveChangesAsync();
    }
}
