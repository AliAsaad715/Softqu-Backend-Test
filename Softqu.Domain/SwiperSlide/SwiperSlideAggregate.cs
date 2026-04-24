using Softqu.Domain.Category;
using Softqu.Domain.Shared;
using Softqu.Domain.Shared.Exceptions;
using Softqu.Domain.SwiperSlide.Entities;
using Softqu.Domain.SwiperSlide.ValueObjects;

namespace Softqu.Domain.SwiperSlide
{
    public class SwiperSlideAggregate : AggregateRoot
    {
        public int SortOrder { get; private set; }
        public string ImageUrl { get; private set; }
        public Guid CategoryId { get; private set; }
        public CategoryAggregate Category { get; private set; }

        private readonly List<SwiperSlideTranslation> _translations = new();
        public IReadOnlyCollection<SwiperSlideTranslation> Translations => _translations.AsReadOnly();

        private SwiperSlideAggregate() { }

        public SwiperSlideAggregate(int sortOrder, string imageUrl, Guid categoryId)
        {
            Validate(sortOrder, imageUrl, categoryId);
            SortOrder = sortOrder;
            ImageUrl = imageUrl;
            CategoryId = categoryId;
        }

        public void AddTranslation(string lang, SlideTexts texts, HighlightedTitle highlightedTitle)
        {
            if (_translations.Any(t => t.LanguageCode == lang))
            {
                throw new DomainException($"Translation for language '{lang}' already exists for this slide.");
            }

            _translations.Add(new SwiperSlideTranslation(lang, texts, highlightedTitle));
        }

        public void UpdateTranslation(string lang, SlideTexts texts, HighlightedTitle highlightedTitle)
        {
            var translation = _translations.FirstOrDefault(t => t.LanguageCode == lang);
            if (translation == null)
            {
                throw new DomainException("Translation not found for the given language code.");
            }
            translation.Update(texts, highlightedTitle);
        }

        public void Validate(int sortOrder, string imageUrl, Guid categoryId)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new DomainException("Image URL cannot be empty.");
            }
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                throw new DomainException("Invalid Image URL format.");
            }
            if (categoryId == Guid.Empty)
            {
                throw new DomainException("Category ID must be provided.");
            }
            if(sortOrder < 0)
            {
                throw new DomainException("Sort order cannot be negative.");
            }
            //if (!_translations.Any())
            //{
            //    throw new DomainException("At least one translation must be added to the slide.");
            //}
        }

        public void Update(string imageUrl, Guid categoryId)
        {
            Validate(SortOrder, imageUrl, categoryId);
            ImageUrl = imageUrl;
            CategoryId = categoryId;
        }

        public void UpdateSortOrder(int sortOrder)
        {
            if (sortOrder <= 0)
                throw new DomainException("SortOrder must be greater than 0");
            SortOrder = sortOrder;
        }
    }
}
