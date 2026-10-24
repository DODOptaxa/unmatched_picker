using WebApplication1.models;

namespace WebApplication1.Contracts;


public record GetMatchupResponce(
    HeroResponce HeroA,
    HeroResponce HeroB
);

public static class MatchupExtensions
{
    public static GetMatchupResponce ToMatchupResponce(this Matchup matchup)
    {
        return new GetMatchupResponce(
            matchup.Hero.ToHeroResponce(),
            matchup.Opponent.ToHeroResponce(true));
    }
}
