using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts;
using WebApplication1.models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/heroes")]
public sealed class HeroController(appdbContext dbContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetHeroesResponce), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetHeroesResponce>> GetHeroes(
        CancellationToken cancellationToken)
    {
        var heroes = await dbContext.Heroes
            .AsNoTracking()
            .OrderBy(hero => hero.Id)
            .Select(hero => hero.ToHeroResponce())
            .ToListAsync(cancellationToken);

        return Ok(new GetHeroesResponce(heroes));
    }
}