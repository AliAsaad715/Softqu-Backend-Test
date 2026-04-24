using MediatR;
using Softqu.Application.Features.Categories.DTOs;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.Categories.Queries
{
    public record GetCategoriesTreeQuery : IRequest<AppResult<List<CategoryDto>>>;
}
