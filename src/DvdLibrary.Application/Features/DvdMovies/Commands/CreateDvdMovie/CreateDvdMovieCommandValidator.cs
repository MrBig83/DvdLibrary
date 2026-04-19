using FluentValidation;

namespace DvdLibrary.Application.Features.DvdMovies.Commands.CreateDvdMovie;

public class CreateDvdMovieCommandValidator : AbstractValidator<CreateDvdMovieCommand>
{
    public CreateDvdMovieCommandValidator()
    {
        RuleFor(x => x.DvdMovie.Title).NotEmpty().MaximumLength(150);
        RuleFor(x => x.DvdMovie.Director).NotEmpty().MaximumLength(100);
        RuleFor(x => x.DvdMovie.ReleaseYear).InclusiveBetween(1900, DateTime.UtcNow.Year + 1);
        RuleFor(x => x.DvdMovie.DurationMinutes).InclusiveBetween(1, 600);
        RuleFor(x => x.DvdMovie.GenreId).GreaterThan(0);
    }
}
