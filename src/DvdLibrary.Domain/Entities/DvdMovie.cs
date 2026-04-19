namespace DvdLibrary.Domain.Entities;

/// <summary>
/// Representerar en DVD-film som går att sälja eller visa i katalogen.
/// </summary>
public class DvdMovie
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Director { get; set; } = string.Empty;

    public int ReleaseYear { get; set; }

    public int DurationMinutes { get; set; }

    public bool IsAvailable { get; set; }

    public int GenreId { get; set; }

    // Varje film tillhör exakt en genre.
    public Genre? Genre { get; set; }
}
