using DvdLibrary.Domain.Entities;

namespace DvdLibrary.Domain.Repositories;

public interface IGenreRepository
{
    Task<IReadOnlyList<Genre>> GetAllAsync(CancellationToken cancellationToken);

    Task<Genre?> GetByIdAsync(int id, CancellationToken cancellationToken);
}
