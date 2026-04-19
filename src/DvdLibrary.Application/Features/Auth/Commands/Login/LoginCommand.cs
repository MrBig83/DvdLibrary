using DvdLibrary.Application.DTOs.Auth;
using MediatR;

namespace DvdLibrary.Application.Features.Auth.Commands.Login;

public record LoginCommand(LoginRequestDto LoginRequest) : IRequest<LoginResponseDto?>;
