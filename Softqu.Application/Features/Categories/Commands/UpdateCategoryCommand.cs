using MediatR;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<AppResult>
    {
        public Guid CategoryId { get; set; }
        public string Slug { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
