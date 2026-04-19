using DvdLibrary.Application.DTOs.DvdMovies;
using MediatR;

namespace DvdLibrary.Application.Features.DvdMovies.Queries.GetAllDvdMovies;

public record GetAllDvdMoviesQuery : IRequest<IReadOnlyList<DvdMovieDto>>;
