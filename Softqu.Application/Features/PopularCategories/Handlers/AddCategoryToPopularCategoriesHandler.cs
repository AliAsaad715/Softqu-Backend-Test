using MediatR;
using Softqu.Application.Features.PopularCategories.Commands;
using Softqu.Application.Shared.Results;
using Softqu.Domain.Category.Interfaces;
using Softqu.Domain.PopularCategory;
using Softqu.Domain.PopularCategroy.Interfaces;

namespace Softqu.Application.Features.PopularCategories.Handlers
{
    public class AddCategoryToPopularCategoriesHandler : IRequestHandler<AddCategoryToPopularCategoriesCommand, AppResult>
    {
        private readonly IPopularCategoryRepository _popularRepo;
        private readonly ICategoryRepository _categoryRepo;

        public AddCategoryToPopularCategoriesHandler(IPopularCategoryRepository popularCategoryRepository, ICategoryRepository categoryRepository)
        {
            _popularRepo = popularCategoryRepository;
            _categoryRepo = categoryRepository;
        }

        public async Task<AppResult> Handle(AddCategoryToPopularCategoriesCommand request, CancellationToken cancellationToken)
        {
            var categoryExists = await _categoryRepo.ExistsAsync(request.CategoryId);
            if (!categoryExists)
            {
                return new NotFound();
            }

            var alreadyPopular = await _popularRepo.ExistCategroyAsync(request.CategoryId);
            if (alreadyPopular)
            {
                return new Forbidden();
            }

            var orderExists = await _popularRepo.ExistOrderAsync(request.SortOrder);
            if (orderExists)
            {
                return new Forbidden();
            }

            var popularCategory = new PopularCategoryAggregate(request.CategoryId, request.SortOrder);
            await _popularRepo.AddAsync(popularCategory);
            await _popularRepo.SaveChangesAsync();
            return new Success();
        }
    }
}
