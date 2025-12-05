using Building_MinimalAPIsMoviesApp.DTOs;
using FluentValidation;

namespace Building_MinimalAPIsMoviesApp.Validations
{
    public class CreateMovieDTOValidator : AbstractValidator<CreateMovieDTO>
    {
        public CreateMovieDTOValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage(ValidationUtilities.NotEmptyMessage)
                .MaximumLength(150)
                .WithMessage(ValidationUtilities.MaximumLengthMessage);
        }
    }
}
