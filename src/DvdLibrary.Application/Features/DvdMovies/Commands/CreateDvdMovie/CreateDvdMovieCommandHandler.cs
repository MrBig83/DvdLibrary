using AutoMapper;
using DvdLibrary.Application.DTOs.DvdMovies;
using DvdLibrary.Domain.Entities;
using DvdLibrary.Domain.Repositories;
using MediatR;

namespace DvdLibrary.Application.Features.DvdMovies.Commands.CreateDvdMovie;

/// <summary>
/// Skapar en ny DVD-film och kopplar den till vald genre.
/// </summary>
public class CreateDvdMovieCommandHandler : IRequestHandler<CreateDvdMovieCommand, DvdMovieDto>
{
    private readonly IDvdMovieRepository _dvdMovieRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDvdMovieCommandHandler(
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

    public async Task<DvdMovieDto> Handle(CreateDvdMovieCommand request, CancellationToken cancellationToken)
    {
        // Genren måste finnas innan filmen sparas.
        var genre = await _genreRepository.GetByIdAsync(request.DvdMovie.GenreId, cancellationToken)
            ?? throw new KeyNotFoundException("Genren kunde inte hittas.");

        // DTO:n översätts till domänentitet innan den skickas till repositoryt.
        var entity = _mapper.Map<DvdMovie>(request.DvdMovie);
        await _dvdMovieRepository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Navigationen sätts för att DTO:n ska kunna returnera genrens namn direkt.
        entity.Genre = genre;
        return _mapper.Map<DvdMovieDto>(entity);
    }
}
