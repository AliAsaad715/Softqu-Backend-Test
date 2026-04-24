using MediatR;
using Softqu.Application.Features.PopularCategories.DTOs;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.PopularCategories.Queries
{
    public record GetPopularCategoriesQuery : IRequest<AppResult<List<PopularCategoryDto>>> { }
}
