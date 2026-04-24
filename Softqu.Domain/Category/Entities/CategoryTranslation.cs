using Softqu.Domain.Shared;
using Softqu.Domain.Shared.Exceptions;

namespace Softqu.Domain.Category.Entities
{
    public class CategoryTranslation : Entity
    {
        public string Title { get; private set; }
        public string LanguageCode { get; private set; }

        private CategoryTranslation() { }

        public CategoryTranslation(string title, string languageCode)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title is required");
            if (string.IsNullOrWhiteSpace(languageCode))
                throw new DomainException("Language code is required");

            Title = title;
            LanguageCode = languageCode;
        }

        public void UpdateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title is required");

            Title = title;
        }
    }
}