using MediatR;
using Softqu.Application.Features.SwiperSlides.Commands;
using Softqu.Application.Shared.Results;
using Softqu.Domain.SwiperSlide.Interfaces;
using Softqu.Domain.SwiperSlide.ValueObjects;

namespace Softqu.Application.Features.SwiperSlides.Handlers
{
    public class UpdateSlideTranslationHandler : IRequestHandler<UpdateSlideTranslationCommand, AppResult>
    {
        private readonly ISwiperSlideRepository _slideRepository;

        public UpdateSlideTranslationHandler(ISwiperSlideRepository repository)
        {
            _slideRepository = repository;
        }

        public async Task<AppResult> Handle(UpdateSlideTranslationCommand request, CancellationToken cancellationToken)
        {
            var slide = await _slideRepository.GetByIdAsync(request.SlideId);
            if (slide == null)
            {
                return new NotFound();
            }
            var translation = slide.Translations.FirstOrDefault(t => t.LanguageCode == request.LanguageCode);
            if (translation == null)
            {
                return new NotFound();
            }

            slide.UpdateTranslation(
                request.LanguageCode,
                new SlideTexts(
                    request.Translation.SlideTexts.TopText,
                    request.Translation.SlideTexts.BigTitle,
                    request.Translation.SlideTexts.BottomText
                ),
                new HighlightedTitle(
                    request.Translation.HighlightedTitle.NormalText,
                    request.Translation.HighlightedTitle.ColorHighlight,
                    request.Translation.HighlightedTitle.BoldHighlight
                )
            );
            await _slideRepository.SaveChangesAsync();
            return new Success();
        }
    }
}
