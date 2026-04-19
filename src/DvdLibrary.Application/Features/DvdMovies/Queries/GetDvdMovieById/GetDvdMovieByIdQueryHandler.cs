using AutoMapper;
using DvdLibrary.Application.DTOs.DvdMovies;
using DvdLibrary.Domain.Repositories;
using MediatR;

namespace DvdLibrary.Application.Features.DvdMovies.Queries.GetDvdMovieById;

public class GetDvdMovieByIdQueryHandler : IRequestHandler<GetDvdMovieByIdQuery, DvdMovieDto?>
{
    private readonly IDvdMovieRepository _dvdMovieRepository;
    private readonly IMapper _mapper;

    public GetDvdMovieByIdQueryHandler(IDvdMovieRepository dvdMovieRepository, IMapper mapper)
    {
        _dvdMovieRepository = dvdMovieRepository;
        _mapper = mapper;
    }

    public async Task<DvdMovieDto?> Handle(GetDvdMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _dvdMovieRepository.GetByIdAsync(request.Id, cancellationToken);
        return movie is null ? null : _mapper.Map<DvdMovieDto>(movie);
    }
}
