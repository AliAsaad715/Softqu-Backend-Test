using FluentValidation;
using Softqu.Application.Features.Categories.Commands;
using Softqu.Domain.Category.Interfaces;

namespace Softqu.Application.Features.Categories.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateNewCategoryCommand>
    {
        private readonly ICategoryRepository _repo;

        public CreateCategoryValidator(ICategoryRepository repo)
        {
            _repo = repo;
            RuleFor(c => c.Slug)
                .NotEmpty()
                .WithMessage("Slug is required.")
                .MustAsync(async (slug, cancellation) =>
                {
                    return !await _repo.AnyWithSlugAsync(slug);
                })
                .WithMessage("This Slug is already in use.");

            RuleFor(c => c.ParentCategoryId)
            .MustAsync(async (parentId, cancellation) =>
            {
                if (parentId == null || parentId == Guid.Empty) return true;
                return await _repo.ExistsAsync(parentId.Value);
            })
            .WithMessage("The Parent Category does not exist.");

            RuleFor(c => c.Translations)
            .NotEmpty().WithMessage("At least one translation is required.")
            .Must(t => t != null && t.Count > 0).WithMessage("Translations list cannot be empty.");

            RuleForEach(c => c.Translations)
                .SetValidator(new CategoryTranslationValidator());
        }
    }
}
