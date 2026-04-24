using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Softqu.Application.Features.Categories.DTOs;
using Softqu.Application.Features.Categories.Mappings;
using Softqu.Application.Features.Categories.Queries;
using Softqu.Application.Shared.Results;
using Softqu.Domain.Category.Interfaces;
using System.Text.Json;

namespace Softqu.Application.Features.Categories.Handlers
{
    public class GetCategoriesTreeHandler : IRequestHandler<GetCategoriesTreeQuery, AppResult<List<CategoryDto>>>
    {
        private readonly ICategoryRepository _repo;
        private readonly IDistributedCache _cache;
        private const string CacheKey = "Categories_Tree_Key";

        public GetCategoriesTreeHandler(ICategoryRepository categoryRepository, IDistributedCache cache)
        {
            _repo = categoryRepository;
            _cache = cache;
        }

        public async Task<AppResult<List<CategoryDto>>> Handle(GetCategoriesTreeQuery request, CancellationToken cancellationToken)
        {
            var cachedData = await _cache.GetStringAsync(CacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedCategories = JsonSerializer.Deserialize<List<CategoryDto>>(cachedData);
                return cachedCategories!;
            }

            var categories = await _repo.GetCategoriesTreeAsync();
            var dtos = categories.Select(c => c.ToDto()).ToList();

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            };

            await _cache.SetStringAsync(
                CacheKey,
                JsonSerializer.Serialize(dtos),
                cacheOptions,
                cancellationToken
            );

            return dtos;
        }
    }
}
