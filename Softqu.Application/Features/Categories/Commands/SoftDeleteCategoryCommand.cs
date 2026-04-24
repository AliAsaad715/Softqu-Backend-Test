using MediatR;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.Categories.Commands
{
    public class SoftDeleteCategoryCommand : IRequest<AppResult>
    {
        public Guid CategoryId { get; set; }
    }
}
