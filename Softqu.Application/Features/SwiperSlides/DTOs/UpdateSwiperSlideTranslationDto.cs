using System.Text.Json.Serialization;

namespace Softqu.Application.Features.SwiperSlides.DTOs
{
    public class UpdateSwiperSlideTranslationDto
    {
        
        public SlideTextsDto SlideTexts { get; set; }
        public HighlightedTitleDto HighlightedTitle { get; set; }
    }
}
