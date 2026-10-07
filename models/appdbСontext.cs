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

    public virtual DbSet<Hero> Heroes { get; set; }

    public virtual DbSet<HeroTip> HeroTips { get; set; }

    public virtual DbSet<Matchup> Matchups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=localhost;port=5467;database=unmatched;username=postgres;password=ilyalihobaba2007 ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("heroes_pkey");

            entity.Property(e => e.WinRate).HasComputedColumnSql("\nCASE\n    WHEN (games_played = 0) THEN NULL::numeric\n    ELSE round((((wins)::numeric * 100.0) / (games_played)::numeric), 2)\nEND", true);
        });

        modelBuilder.Entity<HeroTip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hero_tips_pkey");

            entity.HasOne(d => d.Hero).WithMany(p => p.HeroTips).HasConstraintName("hero_tips_hero_id_fkey");
        });

        modelBuilder.Entity<Matchup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("matchups_pkey");

            entity.Property(e => e.WinRate).HasComputedColumnSql("round((((wins)::numeric * 100.0) / (games)::numeric), 2)", true);

            entity.HasOne(d => d.Hero).WithMany(p => p.MatchupHeroes).HasConstraintName("matchups_hero_id_fkey");

            entity.HasOne(d => d.Opponent).WithMany(p => p.MatchupOpponents).HasConstraintName("matchups_opponent_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
