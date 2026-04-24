namespace Softqu.Application.Features.SwiperSlides.DTOs
{
    public class CreateSwiperSlideTranslationDto
    {
        public string LanguageCode { get; set; }
        public SlideTextsDto SlideTexts { get; set; }
        public HighlightedTitleDto HighlightedTitle { get; set; }
    }
}
