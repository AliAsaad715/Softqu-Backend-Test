using MediatR;
using Softqu.Application.Features.SwiperSlides.DTOs;
using Softqu.Application.Features.SwiperSlides.Mappings;
using Softqu.Application.Features.SwiperSlides.Queries;
using Softqu.Application.Shared.Results;
using Softqu.Domain.SwiperSlide.Interfaces;

namespace Softqu.Application.Features.SwiperSlides.Handlers
{
    public class GetHomeSlidesHandler : IRequestHandler<GetHomeSlidesQuery, AppResult<List<SwiperSlideDto>>>
    {
        private readonly ISwiperSlideRepository _slideRepository;
        public GetHomeSlidesHandler(ISwiperSlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public async Task<AppResult<List<SwiperSlideDto>>> Handle(GetHomeSlidesQuery request, CancellationToken cancellationToken)
        {
            var slides = await _slideRepository.GetSwiperSlidesAsync();
            return slides.Select(s => s.ToDto()).ToList();
        }
    }
}