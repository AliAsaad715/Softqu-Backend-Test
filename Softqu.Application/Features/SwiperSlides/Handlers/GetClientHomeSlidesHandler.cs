using MediatR;
using Softqu.Application.Features.SwiperSlides.DTOs;
using Softqu.Application.Features.SwiperSlides.Mappings;
using Softqu.Application.Features.SwiperSlides.Queries;
using Softqu.Application.Shared.Results;
using Softqu.Domain.SwiperSlide.Interfaces;

namespace Softqu.Application.Features.SwiperSlides.Handlers
{
    public class GetClientHomeSlidesHandler : IRequestHandler<GetClientHomeSlidesQuery, AppResult<List<SwiperSlideClientDto>>>
    {
        private readonly ISwiperSlideRepository _slideRepository;
        public GetClientHomeSlidesHandler(ISwiperSlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public async Task<AppResult<List<SwiperSlideClientDto>>> Handle(GetClientHomeSlidesQuery request, CancellationToken cancellationToken)
        {
            var slides = await _slideRepository.GetSwiperSlidesAsync();

            return slides.Select(s => s.ToClientDto(request.LanguageCode)).ToList();
        }
    }
}
