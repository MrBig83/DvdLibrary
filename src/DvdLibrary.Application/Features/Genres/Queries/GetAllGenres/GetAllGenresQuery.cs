using DvdLibrary.Application.DTOs.Genres;
using MediatR;

namespace DvdLibrary.Application.Features.Genres.Queries.GetAllGenres;

public record GetAllGenresQuery : IRequest<IReadOnlyList<GenreDto>>;
