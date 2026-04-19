using DvdLibrary.Application.DTOs.DvdMovies;
using MediatR;

namespace DvdLibrary.Application.Features.DvdMovies.Commands.UpdateDvdMovie;

public record UpdateDvdMovieCommand(int Id, UpdateDvdMovieDto DvdMovie) : IRequest<DvdMovieDto?>;
