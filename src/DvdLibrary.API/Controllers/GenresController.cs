using DvdLibrary.Application.DTOs.Genres;
using DvdLibrary.Application.Features.Genres.Queries.GetAllGenres;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DvdLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
/// <summary>
/// Exponerar läsning av genrer som stöddata för filmerna.
/// </summary>
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
        // Genre-listan används bland annat när nya filmer ska skapas.
        var result = await _mediator.Send(new GetAllGenresQuery());
        return Ok(result);
    }
}
