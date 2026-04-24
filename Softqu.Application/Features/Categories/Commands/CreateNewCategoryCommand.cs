using MediatR;
using Softqu.Application.Features.Categories.DTOs;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.Categories.Commands
{
    public class CreateNewCategoryCommand : IRequest<AppResult<CreateNewCategoryDto>>
    {
        public string Slug { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public List<CategoryTranslationDto> Translations { get; set; } = new();
    }
}
