using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.models;

[Table("sets")]
[Index("Name", Name = "sets_name_key", IsUnique = true)]
public partial class Set
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [InverseProperty("Set")]
    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    [InverseProperty("Set")]
    public virtual ICollection<Map> Maps { get; set; } = new List<Map>();
}
