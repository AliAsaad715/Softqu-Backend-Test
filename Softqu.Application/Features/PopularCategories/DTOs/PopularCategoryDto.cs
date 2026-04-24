namespace Softqu.Application.Features.PopularCategories.DTOs
{
    public class PopularCategoryDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public int SortOrder { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
    }
}
