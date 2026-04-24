using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Softqu.Application.Features.Categories.Commands;
using Softqu.Application.Features.Categories.DTOs;
using Softqu.Application.Features.Categories.Mappings;
using Softqu.Application.Shared.Results;
using Softqu.Domain.Category.Interfaces;

namespace Softqu.Application.Features.Categories.Handlers
{
    public class CreateNewCategoryHandler : IRequestHandler<CreateNewCategoryCommand, AppResult<CreateNewCategoryDto>>
    {
        private readonly ICategoryRepository _repo;
        private readonly IDistributedCache _cache;
        private const string CacheKey = "Categories_Tree_Key";

        public CreateNewCategoryHandler(ICategoryRepository categoryRepository, IDistributedCache cache)
        {
            _repo = categoryRepository;
            _cache = cache;
        }

        public async Task<AppResult<CreateNewCategoryDto>> Handle(CreateNewCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = request.ToDomain();
            if (category == null)
                throw new Exception("Failed to create category from the provided data.");
            
            await _repo.AddAsync(category);
            await _repo.SaveChangesAsync();
            await _cache.RemoveAsync(CacheKey);
            return new CreateNewCategoryDto { Id = category.Id };
        }
    }
}
