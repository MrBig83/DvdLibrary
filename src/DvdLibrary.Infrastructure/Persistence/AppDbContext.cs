using DvdLibrary.Domain.Entities;
using DvdLibrary.Domain.Enums;
using DvdLibrary.Domain.Repositories;
using DvdLibrary.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;

namespace DvdLibrary.Infrastructure.Persistence;

/// <summary>
/// Samlar tabeller, relationer och seed-data på ett ställe.
/// </summary>
public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<DvdMovie> DvdMovies => Set<DvdMovie>();

    public DbSet<Genre> Genres => Set<Genre>();

    public DbSet<AppUser> AppUsers => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(300);

            entity.HasData(
                new Genre { Id = 1, Name = "Sci-Fi", Description = "Science fiction med framtidsteknik och rymdäventyr." },
                new Genre { Id = 2, Name = "Action", Description = "Högt tempo, konflikter och stora set pieces." },
                new Genre { Id = 3, Name = "Drama", Description = "Karaktärsdriven berättelse med starka känslor." });
        });

        modelBuilder.Entity<DvdMovie>(entity =>
        {
            entity.Property(x => x.Title).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Director).HasMaxLength(100).IsRequired();

            entity.HasOne(x => x.Genre)
                .WithMany(x => x.DvdMovies)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                new DvdMovie { Id = 1, Title = "The Matrix", Director = "The Wachowskis", ReleaseYear = 1999, DurationMinutes = 136, IsAvailable = true, GenreId = 1 },
                new DvdMovie { Id = 2, Title = "Gladiator", Director = "Ridley Scott", ReleaseYear = 2000, DurationMinutes = 155, IsAvailable = true, GenreId = 2 },
                new DvdMovie { Id = 3, Title = "Interstellar", Director = "Christopher Nolan", ReleaseYear = 2014, DurationMinutes = 169, IsAvailable = false, GenreId = 1 });
        });

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.Property(x => x.Username).HasMaxLength(50).IsRequired();
            entity.Property(x => x.PasswordHash).HasMaxLength(200).IsRequired();

            entity.HasIndex(x => x.Username).IsUnique();

            entity.HasData(
                new AppUser { Id = 1, Username = "admin", PasswordHash = DemoPasswordHasher.HashSeedPassword("Admin123!"), Role = UserRole.Admin },
                new AppUser { Id = 2, Username = "user", PasswordHash = DemoPasswordHasher.HashSeedPassword("User123!"), Role = UserRole.User });
        });
    }
}
