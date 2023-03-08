using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AJPPABLAB1.ModelsEF;

public partial class Ajppablab1Context : DbContext
{
    public Ajppablab1Context()
    {
    }

    public Ajppablab1Context(DbContextOptions<Ajppablab1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<KodyPocztoweEf> KodyPocztoweEfs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=GORWPC0008\\SQLDEVELOPER;Database=AJPPABLAB1;User ID=Administrator;Password=cisco123!L;TrustServerCertificate=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KodyPocztoweEf>(entity =>
        {
            entity.ToTable("Kody_pocztowe_EF");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Adres)
                .HasColumnType("text")
                .HasColumnName("adres");
            entity.Property(e => e.KodPocztowy)
                .HasColumnType("text")
                .HasColumnName("kod_pocztowy");
            entity.Property(e => e.Miejscowosc)
                .HasColumnType("text")
                .HasColumnName("miejscowosc");
            entity.Property(e => e.Powiat)
                .HasColumnType("text")
                .HasColumnName("powiat");
            entity.Property(e => e.Wojewodztwo)
                .HasColumnType("text")
                .HasColumnName("wojewodztwo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
