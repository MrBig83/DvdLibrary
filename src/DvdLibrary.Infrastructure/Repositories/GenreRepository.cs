using DvdLibrary.Domain.Entities;
using DvdLibrary.Domain.Repositories;
using DvdLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DvdLibrary.Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;

    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Genre>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Genres
            .AsNoTracking()
            .OrderBy(genre => genre.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Genre?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(genre => genre.Id == id, cancellationToken);
    }
}
