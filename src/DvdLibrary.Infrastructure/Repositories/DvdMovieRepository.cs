using DvdLibrary.Domain.Entities;
using DvdLibrary.Domain.Repositories;
using DvdLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DvdLibrary.Infrastructure.Repositories;

/// <summary>
/// Håller all databasåtkomst för DvdMovie utanför controller och handler.
/// </summary>
public class DvdMovieRepository : IDvdMovieRepository
{
    private readonly AppDbContext _context;

    public DvdMovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<DvdMovie>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.DvdMovies
            .AsNoTracking()
            .Include(movie => movie.Genre)
            .OrderBy(movie => movie.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task<DvdMovie?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.DvdMovies
            .Include(movie => movie.Genre)
            .FirstOrDefaultAsync(movie => movie.Id == id, cancellationToken);
    }

    public async Task AddAsync(DvdMovie dvdMovie, CancellationToken cancellationToken)
    {
        await _context.DvdMovies.AddAsync(dvdMovie, cancellationToken);
    }

    public void Update(DvdMovie dvdMovie)
    {
        _context.DvdMovies.Update(dvdMovie);
    }

    public void Remove(DvdMovie dvdMovie)
    {
        _context.DvdMovies.Remove(dvdMovie);
    }
}
