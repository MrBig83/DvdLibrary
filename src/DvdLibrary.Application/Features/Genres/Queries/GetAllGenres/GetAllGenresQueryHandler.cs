using AutoMapper;
using DvdLibrary.Application.DTOs.Genres;
using DvdLibrary.Domain.Repositories;
using MediatR;

namespace DvdLibrary.Application.Features.Genres.Queries.GetAllGenres;

public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, IReadOnlyList<GenreDto>>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GetAllGenresQueryHandler(IGenreRepository genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<GenreDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        var genres = await _genreRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IReadOnlyList<GenreDto>>(genres);
    }
}
