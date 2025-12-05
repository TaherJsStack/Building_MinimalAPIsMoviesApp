using FluentValidation;
using Building_MinimalAPIsMoviesApp.DTOs;


namespace Building_MinimalAPIsMoviesApp.Validations
{
    public class CreateActorDTOValidator : AbstractValidator<CreateActorDTO>
    {
        public CreateActorDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage(ValidationUtilities.NotEmptyMessage)
                .MaximumLength(150)
                .WithMessage(ValidationUtilities.MaximumLengthMessage);


            var minimumDate = new DateTime(1990, 1, 1);

            RuleFor(p => p.DateOfBirth)
                .GreaterThanOrEqualTo(minimumDate)
                .WithMessage(ValidationUtilities.GreaterThanOrEqualToMessage);

        }


    }
}
