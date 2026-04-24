namespace Softqu.Application.Features.SwiperSlides.DTOs
{
    public class SwiperSlideClientDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public SwiperSlideTranslationClientDto Texts { get; set; }
    }
}
