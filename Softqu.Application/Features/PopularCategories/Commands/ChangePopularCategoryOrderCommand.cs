using MediatR;
using Softqu.Application.Features.PopularCategories.DTOs;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.PopularCategories.Commands
{
    public class ChangePopularCategoryOrderCommand : IRequest<AppResult<List<NewItemOrderDto>>>
    {
        public List<ChangePopularCategoryOrderDto> Items { get; set; }
    }
}
