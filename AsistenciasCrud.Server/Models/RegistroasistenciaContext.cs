using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AsistenciasCrud.Server.Models;

public partial class RegistroasistenciaContext : DbContext
{
    public RegistroasistenciaContext()
    {
    }

    public RegistroasistenciaContext(DbContextOptions<RegistroasistenciaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencia> Asistencias { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia);

            entity.ToTable("asistencias");

            entity.HasOne(d => d.Usuario) 
                  .WithMany(p => p.Asistencias) 
                  .HasForeignKey(d => d.IdUsuario)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_asistencia_usuario");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("usuarios");

            entity.Property(e => e.Nombre).HasMaxLength(40);
            entity.Property(e => e.ApellidoP).HasMaxLength(40);
            entity.Property(e => e.ApellidoM).HasMaxLength(40);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
