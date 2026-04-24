using MediatR;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.PopularCategories.Commands
{
    public class RemoveCategoryFromPopularCategoriesCommand : IRequest<AppResult>
    {
        public Guid Id { get; set; }
    }
}
