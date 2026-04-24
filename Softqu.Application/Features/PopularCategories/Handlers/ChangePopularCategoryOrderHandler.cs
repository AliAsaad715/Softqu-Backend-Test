using MediatR;
using Softqu.Application.Features.PopularCategories.Commands;
using Softqu.Application.Features.PopularCategories.DTOs;
using Softqu.Application.Shared.Results;
using Softqu.Domain.PopularCategroy.Interfaces;

namespace Softqu.Application.Features.PopularCategories.Handlers
{
    public class ChangePopularCategoryOrderHandler : IRequestHandler<ChangePopularCategoryOrderCommand, AppResult<List<NewItemOrderDto>>>
    {
        private readonly IPopularCategoryRepository _popularRepo;

        public ChangePopularCategoryOrderHandler(IPopularCategoryRepository popularCategoryRepository)
        {
            _popularRepo = popularCategoryRepository;
        }

        public async Task<AppResult<List<NewItemOrderDto>>> Handle(ChangePopularCategoryOrderCommand request, CancellationToken cancellationToken)
        {
            var requestedIds = request.Items.Select(x => x.Id).ToList();
            var popularCategories = await _popularRepo.GetByIdsAsync(requestedIds);

            var foundIds = popularCategories.Select(pc => pc.Id).ToList();
            var missingIds = requestedIds.Except(foundIds).ToList();

            if (missingIds.Any())
            {
                return new NotFound();
            }

            var allPopularCategories = await _popularRepo.GetPopularCategoriesAsync();

            if (request.Items.Count != allPopularCategories.Count)
            {
                return new Forbidden();
            }

            var orderDictionary = request.Items.ToDictionary(
                item => item.Id,
                item => item.SortOrder);

            foreach (var popularCategory in popularCategories)
            {
                var newOrder = orderDictionary[popularCategory.Id];
                popularCategory.UpdateSortOrder(newOrder);
            }

            await _popularRepo.SaveChangesAsync();

            var result = popularCategories
                .OrderBy(pc => pc.SortOrder)
                .Select(pc => new NewItemOrderDto
                {
                    Id = pc.Id,
                    SortOrder = pc.SortOrder
                })
                .ToList();

            return result;
        }
    }
}
