using MediatR;
using Softqu.Application.Features.SwiperSlides.Commands;
using Softqu.Application.Features.SwiperSlides.DTOs;
using Softqu.Application.Shared.Results;
using Softqu.Domain.SwiperSlide;
using Softqu.Domain.SwiperSlide.Interfaces;
using Softqu.Domain.SwiperSlide.ValueObjects;

namespace Softqu.Application.Features.SwiperSlides.Handlers
{
    public class CreateNewSlideHandler : IRequestHandler<CreateNewSlideCommand, AppResult<CreateNewSlideDto>>
    {
        private readonly ISwiperSlideRepository _slideRepository;

        public CreateNewSlideHandler(ISwiperSlideRepository repository)
        {
            _slideRepository = repository;
        }

        public async Task<AppResult<CreateNewSlideDto>> Handle(CreateNewSlideCommand request, CancellationToken cancellationToken)
        {
            var slide = new SwiperSlideAggregate(
                request.SortOrder,
                request.ImageUrl,
                request.CategoryId
            );

            foreach (var t in request.Translations)
            {
                slide.AddTranslation(
                    t.LanguageCode,
                    new SlideTexts(
                        t.SlideTexts.TopText,
                        t.SlideTexts.BigTitle,
                        t.SlideTexts.BottomText
                    ),
                    new HighlightedTitle(
                        t.HighlightedTitle.NormalText,
                        t.HighlightedTitle.ColorHighlight,
                        t.HighlightedTitle.BoldHighlight
                    )
                );
            }

            await _slideRepository.AddAsync(slide);
            await _slideRepository.SaveChangesAsync();

            return new CreateNewSlideDto { Id = slide.Id };
        }
    }
}
