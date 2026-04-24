using MediatR;
using Softqu.Application.Features.SwiperSlides.Commands;
using Softqu.Application.Features.SwiperSlides.DTOs;
using Softqu.Application.Shared.Results;
using Softqu.Domain.SwiperSlide.Interfaces;
namespace Softqu.Application.Features.SwiperSlides.Handlers
{
    public class ChangeSlideOrderHandler : IRequestHandler<ChangeSlideOrderCommand, AppResult<List<NewItemOrderDto>>>
    {
        private readonly ISwiperSlideRepository _swiperRepo;

        public ChangeSlideOrderHandler(ISwiperSlideRepository swiperSlideRepository)
        {
            _swiperRepo = swiperSlideRepository;
        }

        public async Task<AppResult<List<NewItemOrderDto>>> Handle(ChangeSlideOrderCommand request, CancellationToken cancellationToken)
        {
            var requestedIds = request.Items.Select(x => x.Id).ToList();
            var slides = await _swiperRepo.GetByIdsAsync(requestedIds);

            var foundIds = slides.Select(pc => pc.Id).ToList();
            var missingIds = requestedIds.Except(foundIds).ToList();

            if (missingIds.Any())
            {
                return new NotFound();
            }

            var allSlides = await _swiperRepo.GetSwiperSlidesAsync();

            if (request.Items.Count != allSlides.Count)
            {
                return new Forbidden();
            }

            var orderDictionary = request.Items.ToDictionary(
                item => item.Id,
                item => item.SortOrder);

            foreach (var slide in slides)
            {
                var newOrder = orderDictionary[slide.Id];
                slide.UpdateSortOrder(newOrder);
            }

            await _swiperRepo.SaveChangesAsync();

            var result = slides
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
