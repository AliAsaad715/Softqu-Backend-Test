using Softqu.Application.Features.SwiperSlides.DTOs;
using Softqu.Domain.SwiperSlide;

namespace Softqu.Application.Features.SwiperSlides.Mappings
{
    public static class SwiperSlideMappings
    {
        public static SwiperSlideDto ToDto(this SwiperSlideAggregate swiper)
        {
            return new SwiperSlideDto
            {
                Id = swiper.Id,
                CategoryId = swiper.CategoryId,
                ImageUrl = swiper.ImageUrl,
                SortOrder = swiper.SortOrder,
                Translations = swiper.Translations.Select(t => new SwiperSlideTranslationDto
                {
                    Id = t.Id,
                    LanguageCode = t.LanguageCode,
                    SlideTexts = new SlideTextsDto
                    {
                        TopText = t.Texts.TopText,
                        BigTitle = t.Texts.BigTitle,
                        BottomText = t.Texts.BottomText
                    },
                    HighlightedTitle = new HighlightedTitleDto
                    {
                        NormalText = t.Title.NormalText,
                        ColorHighlight = t.Title.ColorHighlight,
                        BoldHighlight = t.Title.BoldHighlight
                    }
                }).ToList()
            };
        }

        public static SwiperSlideClientDto ToClientDto(this SwiperSlideAggregate swiper, string lang)
        {
            var translation = swiper.Translations.FirstOrDefault(t => t.LanguageCode == lang)
                              ?? swiper.Translations.FirstOrDefault();

            return new SwiperSlideClientDto
            {
                Id = swiper.Id,
                CategoryId = swiper.CategoryId,
                ImageUrl = swiper.ImageUrl,
                Texts = new SwiperSlideTranslationClientDto
                {
                    SlideTexts = new SlideTextsDto
                    {
                        BigTitle = translation.Texts.BigTitle,
                        BottomText = translation.Texts.BottomText,
                        TopText = translation.Texts.TopText,
                    },
                    HighlightedTitle = new HighlightedTitleDto
                    {
                        BoldHighlight = translation.Title.BoldHighlight,
                        ColorHighlight = translation.Title.ColorHighlight,
                        NormalText = translation.Title.NormalText,
                    }
                }
            };
        }
    }
}
