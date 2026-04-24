namespace Softqu.Application.Features.SwiperSlides.DTOs
{
    public class SwiperSlideTranslationDto
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public SlideTextsDto SlideTexts { get; set; }
        public HighlightedTitleDto HighlightedTitle { get; set; }
    }
}
