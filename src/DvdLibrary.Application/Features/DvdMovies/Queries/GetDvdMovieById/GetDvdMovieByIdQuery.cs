using DvdLibrary.Application.DTOs.DvdMovies;
using MediatR;

namespace DvdLibrary.Application.Features.DvdMovies.Queries.GetDvdMovieById;

public record GetDvdMovieByIdQuery(int Id) : IRequest<DvdMovieDto?>;
