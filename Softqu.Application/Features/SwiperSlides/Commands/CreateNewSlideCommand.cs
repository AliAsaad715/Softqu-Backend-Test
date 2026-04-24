using MediatR;
using Softqu.Application.Features.Categories.DTOs;
using Softqu.Application.Features.SwiperSlides.DTOs;
using Softqu.Application.Shared.Results;

namespace Softqu.Application.Features.SwiperSlides.Commands
{
    public class CreateNewSlideCommand : IRequest<AppResult<CreateNewSlideDto>>
    {
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public int SortOrder { get; set; }
        public List<CreateSwiperSlideTranslationDto> Translations { get; set; }
    }

    
}
