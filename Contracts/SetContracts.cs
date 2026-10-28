using WebApplication1.models;

namespace WebApplication1.Contracts;

public sealed record CharacterResponse(
    int Id,
    string Name,
    string? Slug,
    int? Games,
    int? Wins,
    int? Losses,
    decimal? WinRate);

public sealed record MapResponse(int Id, string Name);

public sealed record SetResponse(
    int Id,
    string Name,
    IReadOnlyCollection<CharacterResponse> Characters,
    IReadOnlyCollection<MapResponse> Maps);

public sealed record GetSetsResponse(IReadOnlyCollection<SetResponse> Sets);

public static class SetContractExtensions
{
    public static CharacterResponse ToResponse(this Character character) =>
        new(character.Id, character.Name, character.Slug, character.Games,
            character.Wins, character.Losses, character.Winrate);

    public static MapResponse ToResponse(this Map map) =>
        new(map.Id, map.Name);

    public static SetResponse ToResponse(this Set set) =>
        new(
            set.Id,
            set.Name,
            set.Characters.OrderBy(character => character.Id)
                .Select(character => character.ToResponse()).ToArray(),
            set.Maps.OrderBy(map => map.Id)
                .Select(map => map.ToResponse()).ToArray());
}
