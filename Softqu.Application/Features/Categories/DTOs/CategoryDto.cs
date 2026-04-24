namespace Softqu.Application.Features.Categories.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public List<CategoryDto> ChildCategories { get; set; }
        public List<CategoryTranslationDto> Translations { get; set; }
    }
}
