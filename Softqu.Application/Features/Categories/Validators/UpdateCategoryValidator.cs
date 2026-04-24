using FluentValidation;
using Softqu.Application.Features.Categories.Commands;
using Softqu.Domain.Category.Interfaces;

namespace Softqu.Application.Features.Categories.Validators
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _repo;

        public UpdateCategoryValidator(ICategoryRepository repo)
        {
            _repo = repo;
            RuleFor(c => c.Slug)
                .NotEmpty()
                .MustAsync(async (command, slug, cancellation) =>
                {
                    return !await _repo.AnyWithSlugExceptAsync(slug, command.CategoryId);
                })
                .WithMessage("This Slug is already in use.");

            RuleFor(c => c.ParentCategoryId)
                .MustAsync(async (parentId, cancellation) =>
                {
                    if (parentId == null || parentId == Guid.Empty) return true;
                    return await _repo.ExistsAsync(parentId.Value);
                })
                .WithMessage("The Parent Category does not exist.");

            RuleFor(c => c)
                .Must(c => c.ParentCategoryId != c.CategoryId)
                .WithMessage("A category cannot be its own parent.");
        }

    }
}
