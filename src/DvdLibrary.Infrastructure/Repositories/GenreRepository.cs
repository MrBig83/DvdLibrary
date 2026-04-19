using DvdLibrary.Domain.Entities;
using DvdLibrary.Domain.Repositories;
using DvdLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DvdLibrary.Infrastructure.Repositories;

/// <summary>
/// Läser genredata från databasen.
/// </summary>
public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;

    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Genre>> GetAllAsync(CancellationToken cancellationToken)
    {
        // AsNoTracking räcker här eftersom queryn bara läser data.
        return await _context.Genres
            .AsNoTracking()
            .OrderBy(genre => genre.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Genre?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        // Används bland annat när filmer ska skapas eller uppdateras.
        return await _context.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(genre => genre.Id == id, cancellationToken);
    }
}
