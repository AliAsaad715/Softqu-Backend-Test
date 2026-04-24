using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Softqu.Application.Features.Categories.Commands;
using Softqu.Application.Shared.Results;
using Softqu.Domain.Category.Interfaces;

namespace Softqu.Application.Features.Categories.Handlers
{
    public class SoftDeleteCategoryHandler : IRequestHandler<SoftDeleteCategoryCommand, AppResult>
    {
        private readonly ICategoryRepository _repo;
        private readonly IDistributedCache _cache;
        private const string CacheKey = "Categories_Tree_Key";

        public SoftDeleteCategoryHandler(ICategoryRepository categoryRepository, IDistributedCache cache)
        {
            _repo = categoryRepository;
            _cache = cache;
        }

        public async Task<AppResult> Handle(SoftDeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repo.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                return new NotFound();
            }

            category.Delete();
            await _repo.SaveChangesAsync();
            await _cache.RemoveAsync(CacheKey);
            return new Success();
        }
    }
}
