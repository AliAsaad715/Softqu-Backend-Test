using MediatR;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.PopularCategories.Commands
{
    public class AddCategoryToPopularCategoriesCommand : IRequest<AppResult>
    {
        public Guid CategoryId { get; set; }
        public int SortOrder { get; set; }
    }
}
