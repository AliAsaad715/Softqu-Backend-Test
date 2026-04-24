using MediatR;
using Softqu.Application.Features.PopularCategories.Commands;
using Softqu.Application.Shared.Results;
using Softqu.Domain.PopularCategroy.Interfaces;

namespace Softqu.Application.Features.PopularCategories.Handlers
{
    public class RemoveCategoryFromPopularCategoriesHandler : IRequestHandler<RemoveCategoryFromPopularCategoriesCommand, AppResult>
    {
        private readonly IPopularCategoryRepository _popularRepo;
        public RemoveCategoryFromPopularCategoriesHandler(IPopularCategoryRepository popularCategoryRepository)
        {
            _popularRepo = popularCategoryRepository;
        }
        public async Task<AppResult> Handle(RemoveCategoryFromPopularCategoriesCommand request, CancellationToken cancellationToken)
        {
            var popularCategory = await _popularRepo.GetByIdAsync(request.Id);
            if (popularCategory == null)
            {
                return new NotFound();
            }

            await _popularRepo.Remove(popularCategory);
            await _popularRepo.SaveChangesAsync();
            return new Success();
        }
    }
}
