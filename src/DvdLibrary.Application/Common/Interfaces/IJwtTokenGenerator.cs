using DvdLibrary.Domain.Entities;

namespace DvdLibrary.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(AppUser user);
}
