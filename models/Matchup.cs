using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.models;

[Keyless]
[Table("matchups")]
[Index("OpponentId", Name = "ix_matchups_opponent")]
public partial class Matchup
{
    [Column("character_id")]
    public int CharacterId { get; set; }

    [Column("opponent_id")]
    public int OpponentId { get; set; }

    [Column("map_id")]
    public int? MapId { get; set; }

    [Column("scope")]
    public string Scope { get; set; } = null!;

    [Column("games")]
    public int Games { get; set; }

    [Column("wins")]
    public int Wins { get; set; }

    [Column("losses")]
    public int Losses { get; set; }

    [Column("winrate")]
    [Precision(5, 4)]
    public decimal Winrate { get; set; }

    [ForeignKey("CharacterId")]
    public virtual Character Character { get; set; } = null!;

    [ForeignKey("MapId")]
    public virtual Map? Map { get; set; }

    [ForeignKey("OpponentId")]
    public virtual Character Opponent { get; set; } = null!;
}
