using MediatR;
using Softqu.Application.Features.PopularCategories.DTOs;
using Softqu.Application.Features.PopularCategories.Mappings;
using Softqu.Application.Features.PopularCategories.Queries;
using Softqu.Application.Shared.Results;
using Softqu.Domain.PopularCategroy.Interfaces;

namespace Softqu.Application.Features.PopularCategories.Handlers
{
    public class GetClientPopularCategoriesHandler : IRequestHandler<GetClientPopularCategoriesQuery, AppResult<List<PopularCategoryClientDto>>>
    {
        private readonly IPopularCategoryRepository _repo;

        public GetClientPopularCategoriesHandler(IPopularCategoryRepository categoryRepository)
        {
            _repo = categoryRepository;
        }

        public async Task<AppResult<List<PopularCategoryClientDto>>> Handle(GetClientPopularCategoriesQuery request, CancellationToken cancellationToken)
        {
            var popularCategories = await _repo.GetPopularCategoriesAsync();

            // Map popularCategories to PopularCategoryDto and return the result
            return popularCategories.Select(pc => pc.ToClientDto(request.LanguageCode)).ToList();
        }
    }
}
