using MediatR;
using Softqu.Application.Features.SwiperSlides.DTOs;
using Softqu.Application.Shared.Results;
using System.Text.Json.Serialization;

namespace Softqu.Application.Features.SwiperSlides.Commands
{
    public class UpdateSlideTranslationCommand : IRequest<AppResult>
    {
        public Guid SlideId { get; set; }

        [JsonIgnore]
        public string? LanguageCode { get; set; }
        public UpdateSwiperSlideTranslationDto Translation { get; set; }
    }
}
