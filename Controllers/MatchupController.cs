using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts;
using WebApplication1.models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/matchups")]
public sealed class MatchupController(appdbContext dbContext, IMatchupService matchupService) : ControllerBase
{
    [HttpGet("{characterAId:int}/{characterBId:int}/{mapId:int}")]
    [ProducesResponseType(typeof(MatchupResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    
    public async Task<ActionResult<MatchupResponse>> GetMatchup(
        int characterAId,
        int characterBId,
        int? mapId,
        CancellationToken cancellationToken)
    {
        if (characterAId <= 0 || characterBId <= 0 ||
            characterAId == characterBId)
        {
            return BadRequest("Two different positive character ids and a positive map id are required.");
        }

        var matchup = await matchupService.GetMatchupAsync(characterAId, characterBId, mapId, cancellationToken);
        
        if (matchup == null)
        {
            return NotFound($"Matchup was not found for characters {characterAId} and {characterBId} on map {mapId}.");
        }


        return Ok(matchup);

    }
}
