using DvdLibrary.Domain.Entities;

namespace DvdLibrary.Domain.Repositories;

public interface IAppUserRepository
{
    Task<AppUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
}
