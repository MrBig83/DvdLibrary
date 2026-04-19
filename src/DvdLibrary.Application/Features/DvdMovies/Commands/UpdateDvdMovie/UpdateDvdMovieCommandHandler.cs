using AutoMapper;
using DvdLibrary.Application.DTOs.DvdMovies;
using DvdLibrary.Domain.Repositories;
using MediatR;

namespace DvdLibrary.Application.Features.DvdMovies.Commands.UpdateDvdMovie;

public class UpdateDvdMovieCommandHandler : IRequestHandler<UpdateDvdMovieCommand, DvdMovieDto?>
{
    private readonly IDvdMovieRepository _dvdMovieRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateDvdMovieCommandHandler(
        IDvdMovieRepository dvdMovieRepository,
        IGenreRepository genreRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _dvdMovieRepository = dvdMovieRepository;
        _genreRepository = genreRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DvdMovieDto?> Handle(UpdateDvdMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dvdMovieRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        var genre = await _genreRepository.GetByIdAsync(request.DvdMovie.GenreId, cancellationToken)
            ?? throw new KeyNotFoundException("Genren kunde inte hittas.");

        _mapper.Map(request.DvdMovie, entity);
        entity.Genre = genre;

        _dvdMovieRepository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<DvdMovieDto>(entity);
    }
}
