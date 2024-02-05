using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Commands;
using MoviesAPI.Infra.Queries;
using MoviesAPI.ViewModels;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("api/movies")]
[Authorize]
public class MovieController(ISender mediator) : Controller
{
    /// <summary>
    /// Gets all movies
    /// </summary>
    /// <param name="query">Filters</param>
    /// <response code="200">Returns the movies</response>
    /// <response code="204">If no movies are found</response>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<MovieViewModel>), 200)]
    [ProducesResponseType(204)]
    public async Task<ActionResult<List<MovieViewModel>>> GetMovies([FromQuery] GetMoviesQuery query)
    {
        var movies = await mediator.Send(query);
        return Ok(movies);
    }

    /// <summary>
    /// Get a movie by its id
    /// </summary>
    /// <param name="id">The id of the movie</param>
    /// <returns>The full movie</returns>
    /// <response code="200">Returns the movie</response>
    /// <response code="204">If the movie is not found</response>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MovieDetailViewModel), 200)]
    [ProducesResponseType(204)]
    public async Task<ActionResult<MovieDetailViewModel>> GetMovie(Guid id)
    {
        var movie = await mediator.Send(new GetMovieDetailQuery(id));
        return Ok(movie);
    }

    /// <summary>
    /// Creates a new movie
    /// </summary>
    /// <param name="command">The movie to create</param>
    /// <returns>The created Movie</returns>
    /// <response code="201">Returns the newly created movie</response>
    /// <response code="400">If the movie creation fails</response>
    [HttpPost]
    [ProducesResponseType(typeof(MovieDetailViewModel), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<MovieDetailViewModel>> CreateMovie([FromBody] CreateMovieCommand command)
    {
        var movieId = await mediator.Send(command);
        if (movieId.HasValue)
        {
            return CreatedAtAction(nameof(GetMovie), new { id = movieId },
                await GetMovieDetail(movieId.Value));
        }

        return BadRequest();
    }

    /// <summary>
    /// Updates a movie
    /// </summary>
    /// <param name="command">The movie to update</param>
    /// <response code="200">Returns the updated movie</response>
    /// <response code="400">If the movie update fails</response>
    [HttpPut]
    [ProducesResponseType(typeof(MovieDetailViewModel), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<MovieDetailViewModel>> UpdateMovie([FromBody] UpdateMovieCommand command)
    {
        var movieHasBeenUpdated = await mediator.Send(command);
        if (movieHasBeenUpdated)
            return Ok(await GetMovieDetail(command.Id));

        return BadRequest();
    }

    /// <summary>
    /// Deletes a movie
    /// </summary>
    /// <param name="id">The id of the movie to delete</param>
    /// <response code="204">If the movie is deleted</response>
    /// <response code="400">If the movie is not found</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> DeleteMovie(Guid id)
    {
        var movieHasBeenDeleted = await mediator.Send(new DeleteMovieCommand(id));
        if (movieHasBeenDeleted)
            return NoContent();

        return BadRequest("Movie not found");
    }

    /// <summary>
    /// Rates a movie
    /// </summary>
    /// <param name="command">The movie to rate</param>
    /// <response code="200">If the movie is rated</response>
    /// <response code="400">If the movie is not found or if the rate is invalid</response>
    [HttpPost("rate")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> RateMovie([FromBody] RateMovieCommand command)
    {
        var movieHasBeenRated = await mediator.Send(command);
        if (movieHasBeenRated)
            return Ok();

        return BadRequest("Movie not found");
    }

    private Task<MovieDetailViewModel?> GetMovieDetail(Guid id)
    {
        return mediator.Send(new GetMovieDetailQuery(id));
    }
}