using WebApplication1.Contracts;

namespace WebApplication1.Services;

public interface IMatchupService
{
    double CalculatePowerRating(double totalMatchupWinrate, int mapWinRate, int mapGamesCount);
    
    Task<MatchupResponse> GetMatchupAsync(int characterAId, int characterBId, int? mapId, CancellationToken cancellationToken);
}