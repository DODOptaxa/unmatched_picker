using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.models;

[Table("hero_tips")]
[Index("HeroId", Name = "hero_tips_hero_id_idx")]
public partial class HeroTip
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("hero_id")]
    public int HeroId { get; set; }

    [Column("text")]
    public string Text { get; set; } = null!;

    [ForeignKey("HeroId")]
    [InverseProperty("HeroTips")]
    public virtual Hero Hero { get; set; } = null!;
}
