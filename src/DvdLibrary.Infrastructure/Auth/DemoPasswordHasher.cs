using System.Security.Cryptography;
using System.Text;
using DvdLibrary.Application.Common.Interfaces;

namespace DvdLibrary.Infrastructure.Auth;

/// <summary>
/// Enkel hashning för skoluppgiften. I produktion bör saltad och adaptiv hashning användas.
/// </summary>
public class DemoPasswordHasher : IPasswordHasher
{
    private const string DemoSalt = "DVD_LIBRARY_STATIC_SALT";

    public string HashPassword(string password)
    {
        return HashInternal(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return HashInternal(password) == passwordHash;
    }

    public static string HashSeedPassword(string password)
    {
        // Samma logik används för seed-data så att login beter sig likadant överallt.
        return HashInternal(password);
    }

    private static string HashInternal(string password)
    {
        // En fast demosalt räcker för uppgiften men är inte lämplig i skarp drift.
        var inputBytes = Encoding.UTF8.GetBytes($"{password}:{DemoSalt}");
        var hashedBytes = SHA256.HashData(inputBytes);
        return Convert.ToHexString(hashedBytes);
    }
}
