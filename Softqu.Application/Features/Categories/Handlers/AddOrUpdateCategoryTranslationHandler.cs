using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Softqu.Application.Features.Categories.Commands;
using Softqu.Application.Shared.Results;
using Softqu.Domain.Category.Entities;
using Softqu.Domain.Category.Interfaces;

namespace Softqu.Application.Features.Categories.Handlers
{
    public class AddOrUpdateCategoryTranslationHandler : IRequestHandler<AddOrUpdateCategoryTranslationCommand, AppResult>
    {
        private readonly ICategoryRepository _repo;
        private readonly IDistributedCache _cache;
        private const string CacheKey = "Categories_Tree_Key";

        public AddOrUpdateCategoryTranslationHandler(ICategoryRepository categoryRepository, IDistributedCache cache)
        {
            _repo = categoryRepository;
            _cache = cache;
        }

        public async Task<AppResult> Handle(AddOrUpdateCategoryTranslationCommand request, CancellationToken cancellationToken)
        {
            var category = await _repo.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                return new NotFound();
            }

            var translation = category.Translations.FirstOrDefault(t => t.LanguageCode.ToLower() == request.LanguageCode.ToLower());
            if (translation == null)
            {
                var newTranslation = new CategoryTranslation(request.Title, request.LanguageCode);

                category.AddTranslation(newTranslation.Title, newTranslation.LanguageCode);
            }
            else
            {
                category.UpdateTranslation(request.Title, request.LanguageCode);
            }

            await _repo.SaveChangesAsync();
            await _cache.RemoveAsync(CacheKey);
            return new Success();
        }
    }
}
