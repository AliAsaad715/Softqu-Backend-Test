using MediatR;
using Softqu.Application.Features.PopularCategories.DTOs;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.PopularCategories.Queries
{
    public class GetClientPopularCategoriesQuery : IRequest<AppResult<List<PopularCategoryClientDto>>>
    {
        public string LanguageCode { get; set; }
    }
}
