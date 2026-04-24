using MediatR;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.SwiperSlides.Commands
{
    public class UpdateSlideCommand : IRequest<AppResult>
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
    }
}
