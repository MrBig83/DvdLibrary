using DvdLibrary.Application.DTOs.Auth;
using DvdLibrary.Application.Features.Auth.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DvdLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// Hanterar enkel inloggning för att demonstrera JWT i projektet.
/// </summary>
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        // Controllern skickar bara vidare requesten till Application-lagret.
        var result = await _mediator.Send(new LoginCommand(loginRequestDto));
        if (result is null)
        {
            return Unauthorized(new { message = "Fel användarnamn eller lösenord." });
        }

        return Ok(result);
    }
}
