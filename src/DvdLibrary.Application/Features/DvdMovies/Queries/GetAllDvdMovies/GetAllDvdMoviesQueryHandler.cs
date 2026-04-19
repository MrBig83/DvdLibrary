using AutoMapper;
using DvdLibrary.Application.DTOs.DvdMovies;
using DvdLibrary.Domain.Repositories;
using MediatR;

namespace DvdLibrary.Application.Features.DvdMovies.Queries.GetAllDvdMovies;

/// <summary>
/// Hämtar hela filmkatalogen som DTO:er.
/// </summary>
public class GetAllDvdMoviesQueryHandler : IRequestHandler<GetAllDvdMoviesQuery, IReadOnlyList<DvdMovieDto>>
{
    private readonly IDvdMovieRepository _dvdMovieRepository;
    private readonly IMapper _mapper;

    public GetAllDvdMoviesQueryHandler(IDvdMovieRepository dvdMovieRepository, IMapper mapper)
    {
        _dvdMovieRepository = dvdMovieRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<DvdMovieDto>> Handle(GetAllDvdMoviesQuery request, CancellationToken cancellationToken)
    {
        // Queryn läser bara data och gör ingen ändring i databasen.
        var movies = await _dvdMovieRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IReadOnlyList<DvdMovieDto>>(movies);
    }
}
