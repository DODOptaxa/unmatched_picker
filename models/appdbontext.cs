using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.models;

public partial class appdbContext : DbContext
{
    public appdbContext()
    {
    }

    public appdbContext(DbContextOptions<appdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Map> Maps { get; set; }

    public virtual DbSet<Matchup> Matchups { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=localhost;port=5467;database=unmatched;username=postgres;password=drowssap");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("characters_pkey");

            entity.HasOne(d => d.Set).WithMany(p => p.Characters).HasConstraintName("characters_set_id_fkey");
        });

        modelBuilder.Entity<Map>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("maps_pkey");

            entity.HasOne(d => d.Set).WithMany(p => p.Maps).HasConstraintName("maps_set_id_fkey");
        });

        modelBuilder.Entity<Matchup>(entity =>
        {
            entity.HasOne(d => d.Character).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("matchups_character_id_fkey");

            entity.HasOne(d => d.Map).WithMany().HasConstraintName("matchups_map_id_fkey");

            entity.HasOne(d => d.Opponent).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("matchups_opponent_id_fkey");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sets_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
