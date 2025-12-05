using Building_MinimalAPIsMoviesApp.DTOs;
using Building_MinimalAPIsMoviesApp.Repositories;
using FluentValidation;

namespace Building_MinimalAPIsMoviesApp.Validations
{
    public class CreateGenreDTOValidator : AbstractValidator<CreateGenreDTO>
    {
        public CreateGenreDTOValidator(IGenresRepository genresRepository)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Custom Not Empty Message Error For {PropertyName}")
                .MaximumLength(150)
                .WithMessage("Custom Maximum Length Message Error For {PropertyName}")
                .Must(FirstLitterIsUppercase)
                .WithMessage("First Litter Is Uppercase Error Message")
                .MustAsync(async (name, _) =>
                {
                    var exists = await genresRepository.Exists(id: 0, name);
                    return !exists;
                }).WithMessage(g => $"a genre with the {g.Name} already exists.")
                ;
        }

        private bool FirstLitterIsUppercase(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            var firestLitter = value[0].ToString();
            return firestLitter == firestLitter.ToUpper();
        }
    }
}
