using MediatR;

namespace DvdLibrary.Application.Features.DvdMovies.Commands.DeleteDvdMovie;

public record DeleteDvdMovieCommand(int Id) : IRequest<bool>;
