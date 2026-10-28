using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts;
using WebApplication1.models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/sets")]
public sealed class SetController(appdbContext dbContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetSetsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetSetsResponse>> GetSets(
        [FromQuery(Name = "setIds")] int[] setIds,
        CancellationToken cancellationToken)
    {
        if (setIds.Length == 0 || setIds.Any(id => id <= 0))
        {
            return BadRequest("At least one positive set id is required.");
        }

        var ids = setIds.Distinct().ToArray();
        var sets = await dbContext.Sets
            .AsNoTracking()
            .Include(set => set.Characters)
            .Include(set => set.Maps)
            .Where(set => ids.Contains(set.Id))
            .OrderBy(set => set.Id)
            .ToListAsync(cancellationToken);

        return Ok(new GetSetsResponse(sets.Select(set => set.ToResponse()).ToArray()));
    }
}
