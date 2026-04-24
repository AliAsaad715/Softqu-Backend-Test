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
    public class GetClientCategoriesTreeHandler : IRequestHandler<GetClientCategoriesTreeQuery, AppResult<List<CategoryClientDto>>>
    {
        private readonly ICategoryRepository _repo;
        private readonly IDistributedCache _cache;
        private string GetCacheKey(string lang) => $"Categories_Tree_{lang}";

        public GetClientCategoriesTreeHandler(ICategoryRepository categoryRepository, IDistributedCache cache)
        {
            _repo = categoryRepository;
            _cache = cache;
        }

        public async Task<AppResult<List<CategoryClientDto>>> Handle(GetClientCategoriesTreeQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = GetCacheKey(request.LanguageCode);
            var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedData))
            {
                var cachedCategories = JsonSerializer.Deserialize<List<CategoryClientDto>>(cachedData);
                return cachedCategories;
            }

            var categories = await _repo.GetCategoriesTreeAsync();

            var dtos = categories.Select(c => c.ToClientDto(request.LanguageCode)).ToList();

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12)
            };

            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(dtos),
                cacheOptions,
                cancellationToken
            );

            return dtos;
        }
    }
}
