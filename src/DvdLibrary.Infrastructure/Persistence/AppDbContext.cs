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
            // Genrer är uppslagsdata som filmerna refererar till.
            entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(300);

            entity.HasData(
                new Genre { Id = 1, Name = "Sci-Fi", Description = "Science fiction med framtidsteknik och rymdäventyr." },
                new Genre { Id = 2, Name = "Action", Description = "Högt tempo, konflikter och stora set pieces." },
                new Genre { Id = 3, Name = "Drama", Description = "Karaktärsdriven berättelse med starka känslor." });
        });

        modelBuilder.Entity<DvdMovie>(entity =>
        {
            // Filmens textfält begränsas för att matcha rimliga databasvärden.
            entity.Property(x => x.Title).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Director).HasMaxLength(100).IsRequired();

            // En film tillhör exakt en genre och får inte lämna genrefältet tomt.
            entity.HasOne(x => x.Genre)
                .WithMany(x => x.DvdMovies)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed-data gör det enkelt att demonstrera API:t direkt efter migration.
            entity.HasData(
                new DvdMovie { Id = 1, Title = "The Matrix", Director = "The Wachowskis", ReleaseYear = 1999, DurationMinutes = 136, IsAvailable = true, GenreId = 1 },
                new DvdMovie { Id = 2, Title = "Gladiator", Director = "Ridley Scott", ReleaseYear = 2000, DurationMinutes = 155, IsAvailable = true, GenreId = 2 },
                new DvdMovie { Id = 3, Title = "Interstellar", Director = "Christopher Nolan", ReleaseYear = 2014, DurationMinutes = 169, IsAvailable = false, GenreId = 1 },
                new DvdMovie { Id = 4, Title = "Blade Runner", Director = "Ridley Scott", ReleaseYear = 1982, DurationMinutes = 117, IsAvailable = true, GenreId = 1 },
                new DvdMovie { Id = 5, Title = "Inception", Director = "Christopher Nolan", ReleaseYear = 2010, DurationMinutes = 148, IsAvailable = true, GenreId = 1 },
                new DvdMovie { Id = 6, Title = "Mad Max: Fury Road", Director = "George Miller", ReleaseYear = 2015, DurationMinutes = 120, IsAvailable = true, GenreId = 2 },
                new DvdMovie { Id = 7, Title = "Die Hard", Director = "John McTiernan", ReleaseYear = 1988, DurationMinutes = 132, IsAvailable = true, GenreId = 2 },
                new DvdMovie { Id = 8, Title = "The Shawshank Redemption", Director = "Frank Darabont", ReleaseYear = 1994, DurationMinutes = 142, IsAvailable = true, GenreId = 3 },
                new DvdMovie { Id = 9, Title = "A Beautiful Mind", Director = "Ron Howard", ReleaseYear = 2001, DurationMinutes = 135, IsAvailable = false, GenreId = 3 },
                new DvdMovie { Id = 10, Title = "The Green Mile", Director = "Frank Darabont", ReleaseYear = 1999, DurationMinutes = 189, IsAvailable = true, GenreId = 3 });
        });

        modelBuilder.Entity<AppUser>(entity =>
        {
            // Användarna är avsiktligt enkla för att JWT-flödet ska vara lätt att förklara.
            entity.Property(x => x.Username).HasMaxLength(50).IsRequired();
            entity.Property(x => x.PasswordHash).HasMaxLength(200).IsRequired();

            entity.HasIndex(x => x.Username).IsUnique();

            // Två seedade användare räcker för att demonstrera RBAC i Swagger.
            entity.HasData(
                new AppUser { Id = 1, Username = "admin", PasswordHash = DemoPasswordHasher.HashSeedPassword("Admin123!"), Role = UserRole.Admin },
                new AppUser { Id = 2, Username = "user", PasswordHash = DemoPasswordHasher.HashSeedPassword("User123!"), Role = UserRole.User });
        });
    }
}
