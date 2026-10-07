using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.models;

[Table("heroes")]
[Index("Slug", Name = "heroes_slug_key", IsUnique = true)]
public partial class Hero
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("slug")]
    public string Slug { get; set; } = null!;

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("games_played")]
    public int GamesPlayed { get; set; }

    [Column("wins")]
    public int Wins { get; set; }

    [Column("losses")]
    public int Losses { get; set; }

    [Column("win_rate")]
    [Precision(5, 2)]
    public decimal? WinRate { get; set; }

    [InverseProperty("Hero")]
    public virtual ICollection<HeroTip> HeroTips { get; set; } = new List<HeroTip>();

    [InverseProperty("Hero")]
    public virtual ICollection<Matchup> MatchupHeroes { get; set; } = new List<Matchup>();

    [InverseProperty("Opponent")]
    public virtual ICollection<Matchup> MatchupOpponents { get; set; } = new List<Matchup>();
}
