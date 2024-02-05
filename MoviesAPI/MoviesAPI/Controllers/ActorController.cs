using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Commands;
using MoviesAPI.Infra.Queries;
using MoviesAPI.ViewModels;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("api/actors")]
[Authorize]
public class ActorController(ISender mediator) : Controller
{
    /// <summary>
    /// Gets a list of actors.
    /// </summary>
    /// <param name="query">Filters to apply</param>
    /// <returns>List of actors matching the <paramref name="query"/></returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<ActorViewModel>), 200)]
    [ProducesResponseType(204)]
    public async Task<ActionResult<List<ActorViewModel>>> GetActors([FromQuery] GetActorsQuery query)
    {
        var actors = await mediator.Send(query);
        return Ok(actors);
    }

    /// <summary>
    /// Gets an actor by its unique identifier.
    /// </summary>
    /// <param name="id">Actor's id</param>
    /// <returns>Actor that matches the id</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ActorDetailViewModel), 200)]
    [ProducesResponseType(204)]
    public async Task<ActionResult<ActorDetailViewModel?>> GetActor(Guid id)
    {
        var actor = await mediator.Send(new GetActorDetailQuery { Id = id });
        return Ok(actor);
    }

    /// <summary>
    /// Creates a new actor.
    /// </summary>
    /// <param name="command">The actor to create</param>
    /// <response code="201">Actor created</response>
    /// <response code="400">Actor not created</response>
    [HttpPost]
    [ProducesResponseType(typeof(ActorDetailViewModel), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ActorDetailViewModel?>> CreateActor([FromBody] CreateActorCommand command)
    {
        var actorId = await mediator.Send(command);
        if (actorId.HasValue)
        {
            return CreatedAtAction(nameof(GetActor), new { id = actorId },
                await mediator.Send(new GetActorDetailQuery { Id = actorId.Value }));
        }

        return BadRequest();
    }

    /// <summary>
    /// Updates an actor.
    /// </summary>
    /// <param name="command">The actor to update</param>
    /// <response code="200">Actor updated</response>
    /// <response code="400">Actor not found</response>
    [HttpPut]
    [ProducesResponseType(typeof(ActorDetailViewModel), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ActorDetailViewModel>> UpdateActor([FromBody] UpdateActorCommand command)
    {
        var actorHasBeenUpdated = await mediator.Send(command);
        if (actorHasBeenUpdated)
            return Ok(await mediator.Send(new GetActorDetailQuery { Id = command.Id }));

        return BadRequest("Actor not found");
    }

    /// <summary>
    /// Deletes an actor.
    /// </summary>
    /// <param name="id">The id of the actor to delete</param>
    /// <response code="204">Actor deleted</response>
    /// <response code="400">Actor not found</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteActor(Guid id)
    {
        var actorHasBeenDeleted = await mediator.Send(new DeleteActorCommand(id));
        if (actorHasBeenDeleted)
            return NoContent();

        return BadRequest("Actor not found");
    }
}