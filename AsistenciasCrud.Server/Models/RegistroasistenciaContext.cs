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

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PK__asistenc__3956DEE6D77FE8D7");

            entity.ToTable("asistencias");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__asistenci__IdUsu__4BAC3F29");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuarios__5B65BF97C638FB1B");

            entity.ToTable("usuarios");

            entity.Property(e => e.ApellidoM).HasMaxLength(40);
            entity.Property(e => e.ApellidoP).HasMaxLength(40);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(40);
            entity.Property(e => e.Telefono).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
