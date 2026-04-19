using DvdLibrary.Application.DTOs.Genres;
using DvdLibrary.Application.Features.Genres.Queries.GetAllGenres;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DvdLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GenreDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllGenresQuery());
        return Ok(result);
    }
}
