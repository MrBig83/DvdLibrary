using DvdLibrary.Domain.Enums;

namespace DvdLibrary.Domain.Entities;

/// <summary>
/// Enkel användarmodell för demo av login och rollbaserad åtkomst.
/// </summary>
public class AppUser
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }
}
