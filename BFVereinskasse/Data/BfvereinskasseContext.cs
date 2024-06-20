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

    public virtual DbSet<Vereinsmitglied> Vereinsmitglieds { get; set; }

    public virtual DbSet<Zahlungen> Zahlungens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=AppDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vereinsmitglied>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Vereinsmitglied");

            entity.Property(e => e.Bild).HasColumnType("image");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nachname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Vorname)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Zahlungen>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Zahlungen");

            entity.Property(e => e.Beschreibung)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Betrag).HasColumnType("money");
            entity.Property(e => e.Datum).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
