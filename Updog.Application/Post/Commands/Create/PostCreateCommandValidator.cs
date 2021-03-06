using Updog.Domain;
using FluentValidation;
using Updog.Application;

namespace Updog.Application {
    /// <summary>
    /// Validator to validate new posts being created.
    /// </summary>
    public sealed class PostCreateCommandValidator : FluentValidatorAdapter<PostCreateCommand> {
        #region Constructor(s)
        public PostCreateCommandValidator() {
            RuleFor(p => p.Data.Type).IsInEnum().WithMessage("Type must be link, or text.");

            RuleFor(p => p.Data.Title).NotNull().WithMessage("Title is required.");
            RuleFor(p => p.Data.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(p => p.Data.Title).MaximumLength(Post.TitleMaxLength).WithMessage($"Title must be {Post.TitleMaxLength} characters or less.");

            RuleFor(p => p.Data.Body).NotNull().WithMessage("Body is required.");
            RuleFor(p => p.Data.Body).NotEmpty().WithMessage("Body is required.");
            RuleFor(p => p.Data.Body).MaximumLength(Post.BodyMaxLength).WithMessage($"Body must be {Post.BodyMaxLength} characters or less.");

            RuleFor(p => p.Space).NotNull().WithMessage("Space is required.");
            RuleFor(p => p.Space).NotEmpty().WithMessage("Space is required.");

            RuleFor(p => p.User).NotNull().WithMessage("User performing the action is null.");
        }
        #endregion
    }
}