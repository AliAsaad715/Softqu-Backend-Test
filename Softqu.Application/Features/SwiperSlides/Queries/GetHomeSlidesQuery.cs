using MediatR;
using Softqu.Application.Features.SwiperSlides.DTOs;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.SwiperSlides.Queries
{
    public record GetHomeSlidesQuery : IRequest<AppResult<List<SwiperSlideDto>>>
    {
    }
}
