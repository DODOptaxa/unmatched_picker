using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.models;

[Table("matchups")]
[Index("OpponentId", Name = "matchups_opponent_id_idx")]
[Index("HeroId", "OpponentId", Name = "matchups_pair_uniq", IsUnique = true)]
public partial class Matchup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("hero_id")]
    public int HeroId { get; set; }

    [Column("opponent_id")]
    public int OpponentId { get; set; }

    [Column("games")]
    public int Games { get; set; }

    [Column("wins")]
    public int Wins { get; set; }

    [Column("win_rate")]
    [Precision(5, 2)]
    public decimal? WinRate { get; set; }

    [ForeignKey("HeroId")]
    [InverseProperty("MatchupHeroes")]
    public virtual Hero Hero { get; set; } = null!;

    [ForeignKey("OpponentId")]
    [InverseProperty("MatchupOpponents")]
    public virtual Hero Opponent { get; set; } = null!;
}
