using DvdLibrary.Application.Common.Interfaces;
using DvdLibrary.Domain.Repositories;
using DvdLibrary.Infrastructure.Auth;
using DvdLibrary.Infrastructure.Configuration;
using DvdLibrary.Infrastructure.Persistence;
using DvdLibrary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DvdLibrary.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Infrastructure kopplar på databas, repositories och tekniska tjänster.
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' saknas.");

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Här binds gränssnitten från de inre lagren till konkreta implementationer.
        services.AddScoped<IDvdMovieRepository, DvdMovieRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());

        // Auth-tjänsterna hålls också här eftersom de är tekniska implementationer.
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, DemoPasswordHasher>();

        return services;
    }
}
