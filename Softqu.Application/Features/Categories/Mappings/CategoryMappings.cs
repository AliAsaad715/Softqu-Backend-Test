using Softqu.Application.Features.Categories.Commands;
using Softqu.Application.Features.Categories.DTOs;
using Softqu.Domain.Category;

namespace Softqu.Application.Features.Categories.Mappings
{
    internal static class CategoryMappings
    {
        public static CategoryDto ToDto(this CategoryAggregate category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Slug = category.Slug,
                ParentCategoryId = category.ParentCategoryId,
                ChildCategories = category.ChildCategories.Select(ToDto).ToList(),
                Translations = category.Translations.Select(t => new CategoryTranslationDto
                {
                    Title = t.Title,
                    LanguageCode = t.LanguageCode
                }).ToList()
            };
        }

        public static CategoryAggregate ToDomain(this CreateNewCategoryCommand category)
        {
            var aggregate = new CategoryAggregate(category.Slug, category.ParentCategoryId);
            if(category.Translations != null)
            {
                foreach (var translation in category.Translations)
                {
                    aggregate.AddTranslation(translation.Title, translation.LanguageCode);
                }
            }
            return aggregate;
        }

        public static CategoryClientDto ToClientDto(this CategoryAggregate category, string lang)
        {
            var translation = category.Translations.FirstOrDefault(t => t.LanguageCode == lang)
                              ?? category.Translations.FirstOrDefault();

            return new CategoryClientDto
            {
                Id = category.Id,
                Name = translation?.Title, 
                Children = category.ChildCategories
                    .Select(child => child.ToClientDto(lang))
                    .ToList() ?? new List<CategoryClientDto>()
            };
        }
    }
}
