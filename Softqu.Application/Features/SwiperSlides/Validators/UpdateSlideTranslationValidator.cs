using FluentValidation;
using Softqu.Application.Features.SwiperSlides.Commands;

namespace Softqu.Application.Features.SwiperSlides.Validators
{
    public class UpdateSlideTranslationValidator : AbstractValidator<UpdateSlideTranslationCommand>
    {
        public UpdateSlideTranslationValidator()
        {
            RuleFor(x => x.SlideId).NotEmpty().WithMessage("Slide ID is required.");
            RuleFor(x => x.Translation).NotNull().WithMessage("Translation data is required.");
            When(x => x.Translation != null, () =>
            {
                RuleFor(x => x.Translation.SlideTexts).NotNull().WithMessage("Slide texts are required.");
                RuleFor(x => x.Translation.SlideTexts.TopText).NotEmpty();
                RuleFor(x => x.Translation.SlideTexts.BigTitle).NotEmpty();
                RuleFor(x => x.Translation.SlideTexts.BottomText).NotEmpty();
                RuleFor(x => x.Translation.HighlightedTitle).NotNull().WithMessage("Highlighted title section is required.");
                RuleFor(x => x.Translation.HighlightedTitle.NormalText).NotEmpty();
                RuleFor(x => x.Translation.HighlightedTitle.ColorHighlight)
                    .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$").WithMessage("Invalid Hex Color format (e.g., #FFFFFF).");
            });
        }
    }
}
