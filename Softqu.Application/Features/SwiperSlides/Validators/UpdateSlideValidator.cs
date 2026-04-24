using FluentValidation;
using Softqu.Application.Features.SwiperSlides.Commands;
using Softqu.Domain.Category.Interfaces;

namespace Softqu.Application.Features.SwiperSlides.Validators
{
    public class UpdateSlideValidator : AbstractValidator<UpdateSlideCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateSlideValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Slide Id is required.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image URL is required.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("Invalid URL format.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category ID is required.")
                .MustAsync(async (categoryId, cancellation) =>
                {
                    return await _categoryRepository.ExistsAsync(categoryId);
                })
                .WithMessage("The specified Category does not exist.");
        }
    }
}
