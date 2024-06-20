using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BFVereinskasse.Data;

public partial class BfvereinskasseContext : DbContext
{
    public BfvereinskasseContext()
    {
    }

    public BfvereinskasseContext(DbContextOptions<BfvereinskasseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mitglied> Mitglieds { get; set; }

    public virtual DbSet<Zahlung> Zahlungs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=AppDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mitglied>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mitglied__3214EC07587C0100");

            entity.ToTable("Mitglied");

            entity.Property(e => e.Bild)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nachname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Vorname)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Zahlung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Zahlung__3214EC07DCD53748");

            entity.ToTable("Zahlung");

            entity.Property(e => e.Beschreibung)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Betrag).HasColumnType("money");
            entity.Property(e => e.Datum).HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithMany(p => p.Zahlungs)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ZahlungMitglied");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
