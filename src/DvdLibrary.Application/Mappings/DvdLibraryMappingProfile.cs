using AutoMapper;
using DvdLibrary.Application.DTOs.DvdMovies;
using DvdLibrary.Application.DTOs.Genres;
using DvdLibrary.Domain.Entities;

namespace DvdLibrary.Application.Mappings;

/// <summary>
/// Samlar alla mappningar centralt för att DTO:er ska hållas isär från entiteter.
/// </summary>
public class DvdLibraryMappingProfile : Profile
{
    public DvdLibraryMappingProfile()
    {
        // Genrer returneras som enkla DTO:er till API:t.
        CreateMap<Genre, GenreDto>();

        // Filmens genrenamn hämtas från navigationen så att klienten slipper slå upp det separat.
        CreateMap<DvdMovie, DvdMovieDto>()
            .ForMember(destination => destination.GenreName, options => options.MapFrom(source => source.Genre != null ? source.Genre.Name : string.Empty));

        // Create och Update mappar bara indata till domänmodellen.
        CreateMap<CreateDvdMovieDto, DvdMovie>();
        CreateMap<UpdateDvdMovieDto, DvdMovie>();
    }
}
