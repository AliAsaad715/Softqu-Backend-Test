using FluentValidation;
using Softqu.Application.Features.SwiperSlides.Commands;
using Softqu.Application.Features.SwiperSlides.DTOs;

namespace Softqu.Application.Features.SwiperSlides.Validators
{
    public class CreateNewSlideValidator : AbstractValidator<CreateNewSlideCommand>
    {
        public CreateNewSlideValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image URL is required.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("Invalid URL format.");

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Sort order must be a non-negative integer.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category ID is required.");

            RuleFor(x => x.Translations)
                .NotEmpty().WithMessage("At least one translation is required.");

            RuleForEach(x => x.Translations).SetValidator(new SlideTranslationValidator());
        }
    }

    public class SlideTranslationValidator : AbstractValidator<CreateSwiperSlideTranslationDto>
    {
        public SlideTranslationValidator()
        {
            RuleFor(x => x.LanguageCode)
                .NotEmpty().WithMessage("Language code is required.")
                .MaximumLength(5).WithMessage("Language code is too long (e.g., 'en', 'ar').");

            RuleFor(x => x.SlideTexts).NotNull().WithMessage("Slide texts are required.");
            RuleFor(x => x.SlideTexts.TopText).NotEmpty();
            RuleFor(x => x.SlideTexts.BigTitle).NotEmpty();
            RuleFor(x => x.SlideTexts.BottomText).NotEmpty();

            RuleFor(x => x.HighlightedTitle).NotNull().WithMessage("Highlighted title section is required.");
            RuleFor(x => x.HighlightedTitle.NormalText).NotEmpty();

            RuleFor(x => x.HighlightedTitle.ColorHighlight)
                .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$").WithMessage("Invalid Hex Color format (e.g., #FFFFFF).");
        }
    }
}
