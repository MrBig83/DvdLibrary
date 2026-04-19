namespace DvdLibrary.Application.DTOs.DvdMovies;

public class DvdMovieDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Director { get; set; } = string.Empty;

    public int ReleaseYear { get; set; }

    public int DurationMinutes { get; set; }

    public bool IsAvailable { get; set; }

    public int GenreId { get; set; }

    public string GenreName { get; set; } = string.Empty;
}
