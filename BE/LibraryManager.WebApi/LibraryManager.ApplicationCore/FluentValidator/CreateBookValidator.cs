using FluentValidation;
using LibraryManager.ApplicationCore.Features.Books.Commands;

namespace LibraryManager.FluentValidator
{
    public class CreateBookValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookValidator()
        {
            RuleFor(z => z.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(100);

            RuleFor(z => z.Author)
                .NotEmpty()
                .WithMessage("Author is required.")
                .MaximumLength(100);

            RuleFor(z => z.ISBN)
                .NotEmpty()
                .WithMessage("ISBN is required.")
                .MaximumLength(13)
                .Matches(@"^\d{10}(\d{3})?$")
                .WithMessage("ISBN must be a valid 10 or 13 digit number.");

            RuleFor(z => z.CopiesAvailable)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Copies available must be greater than or equal to 0.");
        }
    }
}
