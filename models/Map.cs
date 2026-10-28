using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.models;

[Table("maps")]
[Index("SetId", Name = "ix_maps_set")]
[Index("Name", Name = "maps_name_key", IsUnique = true)]
public partial class Map
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("set_id")]
    public int? SetId { get; set; }

    [ForeignKey("SetId")]
    [InverseProperty("Maps")]
    public virtual Set? Set { get; set; }
}
