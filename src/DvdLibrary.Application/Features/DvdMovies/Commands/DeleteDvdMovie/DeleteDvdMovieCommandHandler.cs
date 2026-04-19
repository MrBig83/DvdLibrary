using DvdLibrary.Domain.Repositories;
using MediatR;

namespace DvdLibrary.Application.Features.DvdMovies.Commands.DeleteDvdMovie;

/// <summary>
/// Tar bort en DVD-film om den finns i databasen.
/// </summary>
public class DeleteDvdMovieCommandHandler : IRequestHandler<DeleteDvdMovieCommand, bool>
{
    private readonly IDvdMovieRepository _dvdMovieRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDvdMovieCommandHandler(IDvdMovieRepository dvdMovieRepository, IUnitOfWork unitOfWork)
    {
        _dvdMovieRepository = dvdMovieRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteDvdMovieCommand request, CancellationToken cancellationToken)
    {
        // Null betyder att filmen saknas och därför inte kan tas bort.
        var entity = await _dvdMovieRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _dvdMovieRepository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
