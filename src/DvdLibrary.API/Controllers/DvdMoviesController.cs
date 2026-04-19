using DvdLibrary.Application.DTOs.DvdMovies;
using DvdLibrary.Application.Features.DvdMovies.Commands.CreateDvdMovie;
using DvdLibrary.Application.Features.DvdMovies.Commands.DeleteDvdMovie;
using DvdLibrary.Application.Features.DvdMovies.Commands.UpdateDvdMovie;
using DvdLibrary.Application.Features.DvdMovies.Queries.GetAllDvdMovies;
using DvdLibrary.Application.Features.DvdMovies.Queries.GetDvdMovieById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DvdLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DvdMoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DvdMoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DvdMovieDto>>> GetAll()
    {
        var result = await _mediator.Send(new GetAllDvdMoviesQuery());
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DvdMovieDto>> GetById(int id)
    {
        var result = await _mediator.Send(new GetDvdMovieByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<DvdMovieDto>> Create([FromBody] CreateDvdMovieDto createDvdMovieDto)
    {
        var result = await _mediator.Send(new CreateDvdMovieCommand(createDvdMovieDto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<DvdMovieDto>> Update(int id, [FromBody] UpdateDvdMovieDto updateDvdMovieDto)
    {
        var result = await _mediator.Send(new UpdateDvdMovieCommand(id, updateDvdMovieDto));
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _mediator.Send(new DeleteDvdMovieCommand(id));
        return deleted ? NoContent() : NotFound();
    }
}
