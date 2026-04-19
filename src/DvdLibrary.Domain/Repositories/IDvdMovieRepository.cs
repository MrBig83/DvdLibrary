using DvdLibrary.Domain.Entities;

namespace DvdLibrary.Domain.Repositories;

public interface IDvdMovieRepository
{
    Task<IReadOnlyList<DvdMovie>> GetAllAsync(CancellationToken cancellationToken);

    Task<DvdMovie?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAsync(DvdMovie dvdMovie, CancellationToken cancellationToken);

    void Update(DvdMovie dvdMovie);

    void Remove(DvdMovie dvdMovie);
}
