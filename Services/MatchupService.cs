using WebApplication1.models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Services;
using WebApplication1.Contracts;

public class MatchupService(appdbContext dbContext): IMatchupService
{
    public double CalculatePowerRating(double totalWinrate, int mapWins, int mapGamesCount)
    {
        // Защита от нулевого количества игр: если боев не было, возвращаем общий винрейт
        if (mapGamesCount <= 0)
        {
            return (double)totalWinrate;
        }

        // Сырой винрейт на конкретной карте
        double rawMapWinrate = (double)mapWins / mapGamesCount;

        // Формула Байеса: (C * WR_total + R_map * N_map) / (C + N_map)
        double adjustedWinrate = (7 * (double)totalWinrate + rawMapWinrate * mapGamesCount) 
                                 / (7 + mapGamesCount);

        return Math.Round(adjustedWinrate, 4);
    }
    
    public async Task<MatchupResponse> GetMatchupAsync(int characterAId, int characterBId, int? mapId, CancellationToken cancellationToken)
    {
        var matchup = await dbContext.Matchups
            .AsNoTracking()
            .Where(m => m.CharacterId == characterAId && m.OpponentId == characterBId && m.MapId == mapId)
            .FirstOrDefaultAsync(cancellationToken);

        if (matchup is null) return null;
        
        var powerRating = (double)matchup.Winrate; 
        
        if (mapId != null)
        {
            matchup = await dbContext.Matchups
                .AsNoTracking()
                .Where(m => m.CharacterId == characterAId && m.OpponentId == characterBId && m.MapId == mapId) 
                .FirstOrDefaultAsync(cancellationToken);
            
            powerRating = CalculatePowerRating(powerRating, matchup.Wins, matchup.Games);
        }
        
        
        return matchup.ToResponse(powerRating);
    }
}