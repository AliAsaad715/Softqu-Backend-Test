namespace Softqu.Application.Features.SwiperSlides.DTOs
{
    public class SwiperSlideDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public int SortOrder { get; set; }
        public Guid CategoryId { get; set; }
        public List<SwiperSlideTranslationDto> Translations { get; set; }
    }
}
