using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.models;

[Table("characters")]
[Index("Name", Name = "characters_name_key", IsUnique = true)]
[Index("SetId", Name = "ix_characters_set")]
public partial class Character
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("slug")]
    public string? Slug { get; set; }

    [Column("set_id")]
    public int? SetId { get; set; }

    [Column("games")]
    public int? Games { get; set; }

    [Column("wins")]
    public int? Wins { get; set; }

    [Column("losses")]
    public int? Losses { get; set; }

    [Column("winrate")]
    [Precision(5, 4)]
    public decimal? Winrate { get; set; }

    [ForeignKey("SetId")]
    [InverseProperty("Characters")]
    public virtual Set? Set { get; set; }
}
