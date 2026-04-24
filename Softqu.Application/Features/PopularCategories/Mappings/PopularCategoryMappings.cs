using Softqu.Application.Features.Categories.DTOs;
using Softqu.Application.Features.PopularCategories.DTOs;
using Softqu.Domain.Category;
using Softqu.Domain.PopularCategory;

namespace Softqu.Application.Features.PopularCategories.Mappings
{
    internal static class PopularCategoryMappings
    {
        public static PopularCategoryDto ToDto(this PopularCategoryAggregate aggregate)
        {
            return new PopularCategoryDto
            {
                Id = aggregate.Id,
                CategoryId = aggregate.CategoryId,
                SortOrder = aggregate.SortOrder,
                Slug = aggregate.Category.Slug,
                Title = aggregate.Category.Translations.FirstOrDefault(t => t.LanguageCode == "en")?.Title
                      ?? aggregate.Category.Translations.FirstOrDefault()?.Title
            };
        }

        public static PopularCategoryClientDto ToClientDto(this PopularCategoryAggregate popularCategory, string lang)
        {
            var translation = popularCategory.Category.Translations.FirstOrDefault(t => t.LanguageCode == lang)
                              ?? popularCategory.Category.Translations.FirstOrDefault();

            return new PopularCategoryClientDto
            {
                Id = popularCategory.Id,
                Title = translation?.Title,
                CategoryId = popularCategory.CategoryId,
            };
        }
    }
}
