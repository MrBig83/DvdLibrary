namespace DvdLibrary.Domain.Entities;

/// <summary>
/// Representerar en genre som flera DVD-filmer kan kopplas till.
/// </summary>
public class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // Navigationen visar 1-till-många-relationen mellan Genre och DvdMovie.
    public ICollection<DvdMovie> DvdMovies { get; set; } = new List<DvdMovie>();
}
