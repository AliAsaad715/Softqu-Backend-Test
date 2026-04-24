using Softqu.Domain.Category.Entities;
using Softqu.Domain.Shared;
using Softqu.Domain.Shared.Exceptions;

namespace Softqu.Domain.Category
{
    public class CategoryAggregate : AggregateRoot
    {
        public string Slug { get; private set; }
        public Guid? ParentCategoryId { get; private set; }

        private readonly List<CategoryAggregate> _childCategories = new();
        public IReadOnlyCollection<CategoryAggregate> ChildCategories => _childCategories.AsReadOnly();

        private readonly List<CategoryTranslation> _translations = new();
        public IReadOnlyCollection<CategoryTranslation> Translations => _translations.AsReadOnly();

        private CategoryAggregate() { }
        public CategoryAggregate(string slug, Guid? parentCategoryId = null)
        {
            if(string.IsNullOrWhiteSpace(slug))
                throw new DomainException("Slug is required");

            Slug = slug;
            ParentCategoryId = parentCategoryId;
        }

        public void Update(string slug, Guid? parentCategoryId)
        {
            if(string.IsNullOrWhiteSpace(slug))
                throw new DomainException("Slug is required");
            if (parentCategoryId == Id)
                throw new DomainException("Category cannot be its own parent.");

            Slug = slug;
            ParentCategoryId = parentCategoryId;
        }

        public void AddTranslation(string title, string languageCode)
        {
            if(_translations.Any(x => x.LanguageCode.ToLower() == languageCode.ToLower()))
            {
                throw new DomainException($"Translation for language '{languageCode}' already exists for this category.");
            }

            _translations.Add(new CategoryTranslation(title, languageCode));
        }

        public void UpdateTranslation(string title, string languageCode)
        {
            var translation = _translations.FirstOrDefault(x => x.LanguageCode.ToLower() == languageCode.ToLower());
            if(translation == null)
            {
                throw new DomainException($"Translation for language '{languageCode}' does not exist for this category.");
            }
            translation.UpdateTitle(title);
        }

        public override void Delete()
        {
            if (_childCategories.Any(c => !c.IsDeleted))
            {
                throw new DomainException("Cannot delete category with active sub-categories.");
            }

            base.Delete();
        }
    }
}
