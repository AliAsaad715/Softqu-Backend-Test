using MediatR;
using Softqu.Application.Features.SwiperSlides.DTOs;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.SwiperSlides.Commands
{
    public class ChangeSlideOrderCommand : IRequest<AppResult<List<NewItemOrderDto>>>
    {
        public List<ChangeSlideOrderDto> Items {  get; set; }
    }
}
