namespace Softqu.Application.Features.Categories.DTOs
{
    public class CategoryClientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<CategoryClientDto> Children { get; set; }
    }
}
