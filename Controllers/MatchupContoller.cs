using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts;
using WebApplication1.models;


namespace WebApplication1.Controllers;

[ApiController]
[Route("api/matchups")]
public sealed class MatchupController(appdbContext dbContext) : ControllerBase
{
    [HttpGet("{heroAId:int}/{heroBId:int}")]
    [ProducesResponseType(typeof(GetMatchupResponce), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetMatchupResponce>> GetMatchup(
        int heroAId,
        int heroBId,
        CancellationToken cancellationToken)
    {
        if (heroAId <= 0 || heroBId <= 0 || heroAId == heroBId)
        {
            return BadRequest("Two different positive hero ids are required.");
        }

        var matchup = await dbContext.Matchups.FirstOrDefaultAsync(
            m => (m.HeroId == heroAId && m.OpponentId == heroBId) || (m.HeroId == heroBId && m.OpponentId == heroAId),
            cancellationToken);

        if (matchup is null)
        {
            return NotFound($"Matchup was not found for heroes {heroAId} and {heroBId}.");
        }

        return Ok(matchup.ToMatchupResponce());
    }
}