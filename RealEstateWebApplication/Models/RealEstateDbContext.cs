using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RealEstateWebApplication.Models;

public partial class RealEstateDbContext : DbContext
{
    public RealEstateDbContext()
    {
    }

    public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Realty> Realties { get; set; }

    public virtual DbSet<RealtyOwner> RealtyOwners { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server= DESKTOP-AIS15SI\\SQLEXPRESS; Database=RealEstateDB; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>(entity =>
        {
            entity.ToTable("Owner");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<Realty>(entity =>
        {
            entity.ToTable("Realty");

            entity.HasOne(d => d.Type).WithMany(p => p.Realties)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Realty_Type");
        });

        modelBuilder.Entity<RealtyOwner>(entity =>
        {
            entity.ToTable("RealtyOwner");

            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Owner).WithMany(p => p.RealtyOwners)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK_RealtyOwner_Owner");

            entity.HasOne(d => d.Realty).WithMany(p => p.RealtyOwners)
                .HasForeignKey(d => d.RealtyId)
                .HasConstraintName("FK_RealtyOwner_Realty");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.ToTable("Type");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
