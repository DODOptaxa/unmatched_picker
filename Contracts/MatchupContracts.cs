using WebApplication1.models;
using WebApplication1.Services;

namespace WebApplication1.Contracts;

public sealed record MatchupCharacterResponse(
    int Id,
    string Name,
    double PowerRating);

public sealed record MatchupResponse(
    MapResponse Map,
    MatchupCharacterResponse CharacterA,
    MatchupCharacterResponse CharacterB);

public static class MatchupContractExtensions
{
    public static MatchupResponse ToResponse(
        this Matchup matchup,
        double characterPowerRating) =>
        new(
            matchup.Map?.ToResponse(),
            matchup.Character.ToResponse(characterPowerRating),
            matchup.Opponent.ToResponse(100 - characterPowerRating));
    
    public static MatchupCharacterResponse ToResponse(
        this Character character,
        double powerRating) =>
        new(character.Id, character.Name, powerRating);
}
