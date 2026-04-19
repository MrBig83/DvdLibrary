using DvdLibrary.Application.DTOs.DvdMovies;
using MediatR;

namespace DvdLibrary.Application.Features.DvdMovies.Commands.CreateDvdMovie;

public record CreateDvdMovieCommand(CreateDvdMovieDto DvdMovie) : IRequest<DvdMovieDto>;
