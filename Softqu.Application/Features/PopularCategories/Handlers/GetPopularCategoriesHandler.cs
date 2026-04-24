using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Softqu.Application.Features.PopularCategories.DTOs;
using Softqu.Application.Features.PopularCategories.Mappings;
using Softqu.Application.Features.PopularCategories.Queries;
using Softqu.Application.Shared.Results;
using Softqu.Domain.PopularCategroy.Interfaces;

namespace Softqu.Application.Features.PopularCategories.Handlers
{
    public class GetPopularCategoriesHandler : IRequestHandler<GetPopularCategoriesQuery, AppResult<List<PopularCategoryDto>>>
    {
        private readonly IPopularCategoryRepository _repo;

        public GetPopularCategoriesHandler(IPopularCategoryRepository categoryRepository)
        {
            _repo = categoryRepository;
        }

        public async Task<AppResult<List<PopularCategoryDto>>> Handle(GetPopularCategoriesQuery request, CancellationToken cancellationToken)
        {
            var popularCategories = await _repo.GetPopularCategoriesAsync();

            // Map popularCategories to PopularCategoryDto and return the result
            return popularCategories.Select(pc => pc.ToDto()).ToList();
        }
    }
}
