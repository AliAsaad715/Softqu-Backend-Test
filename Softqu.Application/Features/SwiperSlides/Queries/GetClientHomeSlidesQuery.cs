using MediatR;
using Softqu.Application.Features.SwiperSlides.DTOs;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.SwiperSlides.Queries
{
    public class GetClientHomeSlidesQuery : IRequest<AppResult<List<SwiperSlideClientDto>>>
    {
        public string LanguageCode { get; set; }
    }
}
