using DvdLibrary.Domain.Entities;
using DvdLibrary.Domain.Repositories;
using DvdLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DvdLibrary.Infrastructure.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly AppDbContext _context;

    public AppUserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AppUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.AppUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Username == username, cancellationToken);
    }
}
