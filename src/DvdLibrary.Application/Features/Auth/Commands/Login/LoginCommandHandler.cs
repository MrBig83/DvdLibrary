using DvdLibrary.Application.Common.Interfaces;
using DvdLibrary.Application.DTOs.Auth;
using DvdLibrary.Domain.Repositories;
using MediatR;

namespace DvdLibrary.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto?>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(
        IAppUserRepository appUserRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _appUserRepository = appUserRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _appUserRepository.GetByUsernameAsync(request.LoginRequest.Username, cancellationToken);
        if (user is null || !_passwordHasher.VerifyPassword(request.LoginRequest.Password, user.PasswordHash))
        {
            return null;
        }

        return new LoginResponseDto
        {
            Token = _jwtTokenGenerator.GenerateToken(user),
            Username = user.Username,
            Role = user.Role.ToString()
        };
    }
}
