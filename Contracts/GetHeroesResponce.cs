using WebApplication1.models;

namespace WebApplication1.Contracts;

public record HeroResponce(
    int Id,
    string Name,
    string WinRate
);

public record GetHeroesResponce(
    IEnumerable<HeroResponce> Heroes
);

public static class HeroExtensions
{
    public static HeroResponce ToHeroResponce(this Hero hero, bool reverseWinrate = false)
    {
        return new HeroResponce(
            hero.Id,
            hero.Name,
            hero.WinRate.HasValue
                ? (reverseWinrate
                    ? (100 - hero.WinRate.Value).ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)
                    : hero.WinRate.Value.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture))
                : "0");
    }

    public static HeroResponce ToHeroResponce(this Matchup matchup)
    {
        return new HeroResponce(
            matchup.Hero.Id,
            matchup.Hero.Name,
            matchup.WinRate.HasValue
                ? matchup.WinRate.Value.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)
                : "0");
    }
}