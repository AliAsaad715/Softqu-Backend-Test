using Softqu.Domain.Shared;
using Softqu.Domain.Shared.Exceptions;
using Softqu.Domain.SwiperSlide.ValueObjects;

namespace Softqu.Domain.SwiperSlide.Entities
{
    public class SwiperSlideTranslation : Entity
    {
        public string LanguageCode { get; private set; }
        public SlideTexts Texts { get; private set; }
        public HighlightedTitle Title { get; private set; }

        private SwiperSlideTranslation() { }

        public SwiperSlideTranslation(string languageCode, SlideTexts texts, HighlightedTitle title)
        {
            Validate(languageCode, texts, title);
            LanguageCode = languageCode;
            Texts = texts;
            Title = title;
        }

        public void Update(SlideTexts texts, HighlightedTitle title)
        {
            Validate(LanguageCode, texts, title);
            Texts = texts;
            Title = title;
        }

        public void Validate(string language, SlideTexts texts, HighlightedTitle title)
        {
            if (string.IsNullOrWhiteSpace(language))
            {
                throw new DomainException("Language code cannot be empty.");
            }
            if (texts == null)
            {
                throw new DomainException("Slide texts cannot be null.");
            }
            if (title == null)
            {
                throw new DomainException("Highlighted title cannot be null.");
            }
        }
    }
}