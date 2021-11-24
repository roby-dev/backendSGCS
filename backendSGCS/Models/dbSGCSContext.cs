using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backendSGCS.Models
{
    public partial class dbSGCSContext : DbContext
    {
        public dbSGCSContext()
        {
        }

        public dbSGCSContext(DbContextOptions<dbSGCSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cargo> Cargo { get; set; } = null!;
        public virtual DbSet<ElementoConfiguracion> ElementoConfiguracion { get; set; } = null!;
        public virtual DbSet<Entregable> Entregable { get; set; } = null!;
        public virtual DbSet<FaseMetodologia> FaseMetodologia { get; set; } = null!;
        public virtual DbSet<LineaBase> LineaBase { get; set; } = null!;
        public virtual DbSet<Metodologia> Metodologia { get; set; } = null!;
        public virtual DbSet<MiembroProyecto> MiembroProyecto { get; set; } = null!;
        public virtual DbSet<Proyecto> Proyecto { get; set; } = null!;
        public virtual DbSet<Solicitud> Solicitud { get; set; } = null!;
        public virtual DbSet<Usuario> Usuario { get; set; } = null!;
        public virtual DbSet<VersionElementoConfiguracion> VersionElementoConfiguracion { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=tcp:200.106.124.168,1433;Initial Catalog=dbSGCS;Persist Security Info=True;User ID=roby;Password=Sistemas.123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.HasKey(e => e.IdCargo)
                    .HasName("PK_CARGO");

                entity.HasIndex(e => e.Descripcion, "UQ_NameCargo")
                    .IsUnique();

                entity.Property(e => e.IdCargo).HasColumnName("idCargo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<ElementoConfiguracion>(entity =>
            {
                entity.HasKey(e => e.IdElementoConfiguracion)
                    .HasName("PK_ELEMENTOCONFIGURACION");

                entity.Property(e => e.IdElementoConfiguracion).HasColumnName("idElementoConfiguracion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");                

                entity.Property(e => e.IdLineaBase).HasColumnName("idLineaBase");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdLineaBaseNavigation)
                    .WithMany(p => p.ElementoConfiguracion)
                    .HasForeignKey(d => d.IdLineaBase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ElementoConfiguracion_LineaBase");
            });

            modelBuilder.Entity<Entregable>(entity =>
            {
                entity.HasKey(e => e.IdEntregable)
                    .HasName("PK_ENTREGABLE");

                entity.Property(e => e.IdEntregable).HasColumnName("idEntregable");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdFaseMetodologia).HasColumnName("idFaseMetodologia");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Nomenclatura)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nomenclatura");

                entity.HasOne(d => d.IdFaseMetodologiaNavigation)
                    .WithMany(p => p.Entregable)
                    .HasForeignKey(d => d.IdFaseMetodologia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Entregable_fk0");
            });

            modelBuilder.Entity<FaseMetodologia>(entity =>
            {
                entity.HasKey(e => e.IdFaseMetodologia)
                    .HasName("PK_FASEMETODOLOGIA");

                entity.Property(e => e.IdFaseMetodologia).HasColumnName("idFaseMetodologia");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdMetodologia).HasColumnName("idMetodologia");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdMetodologiaNavigation)
                    .WithMany(p => p.FaseMetodologia)
                    .HasForeignKey(d => d.IdMetodologia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FaseMetodologia_fk0");
            });

            modelBuilder.Entity<LineaBase>(entity =>
            {
                entity.HasKey(e => e.IdLineaBase)
                    .HasName("PK_LINEABASE");

                entity.Property(e => e.IdLineaBase).HasColumnName("idLineaBase");

                entity.Property(e => e.FechaFin)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("fechaFin");

                entity.Property(e => e.FechaInicio)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("fechaInicio");

                entity.Property(e => e.IdEntregable).HasColumnName("idEntregable");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.HasOne(d => d.IdEntregableNavigation)
                    .WithMany(p => p.LineaBase)
                    .HasForeignKey(d => d.IdEntregable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineaBase_Entregable");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.LineaBase)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LineaBase_fk0");
            });

            modelBuilder.Entity<Metodologia>(entity =>
            {
                entity.HasKey(e => e.IdMetodologia)
                    .HasName("PK_METODOLOGIA");

                entity.Property(e => e.IdMetodologia).HasColumnName("idMetodologia");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<MiembroProyecto>(entity =>
            {
                entity.HasKey(e => e.IdMiembroProyecto)
                    .HasName("PK_MIEMBROPROYECTO");

                entity.Property(e => e.IdMiembroProyecto).HasColumnName("idMiembroProyecto");

                entity.Property(e => e.IdCargo).HasColumnName("idCargo");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdCargoNavigation)
                    .WithMany(p => p.MiembroProyecto)
                    .HasForeignKey(d => d.IdCargo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MiembroProyecto_fk2");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.MiembroProyecto)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MiembroProyecto_fk1");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.MiembroProyecto)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MiembroProyecto_fk0");
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.IdProyecto)
                    .HasName("PK_PROYECTO");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaFin)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("fechaFin");

                entity.Property(e => e.FechaInicio)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("fechaInicio");

                entity.Property(e => e.IdMetodologia).HasColumnName("idMetodologia");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Repositorio)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("repositorio");

                entity.HasOne(d => d.IdMetodologiaNavigation)
                    .WithMany(p => p.Proyecto)
                    .HasForeignKey(d => d.IdMetodologia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Proyecto_fk0");
            });

            modelBuilder.Entity<Solicitud>(entity =>
            {
                entity.HasKey(e => e.IdSolicitud)
                    .HasName("PK_SOLICITUD");

                entity.Property(e => e.IdSolicitud).HasColumnName("idSolicitud");

                entity.Property(e => e.Archivo)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("archivo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("fecha");

                entity.Property(e => e.IdElementoConfiguracion).HasColumnName("idElementoConfiguracion");

                entity.Property(e => e.IdMiembroProyecto).HasColumnName("idMiembroProyecto");

                entity.Property(e => e.Objetivo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("objetivo");

                entity.Property(e => e.Respuesta)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("respuesta");

                entity.HasOne(d => d.IdElementoConfiguracionNavigation)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.IdElementoConfiguracion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Solicitud_ElementoConfiguracion");

                entity.HasOne(d => d.IdMiembroProyectoNavigation)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.IdMiembroProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Solicitud_MiembroProyecto");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK_USUARIO");

                entity.HasIndex(e => e.Correo, "UQ__Usuario__2A586E0B7635ACCA")
                    .IsUnique();

                entity.HasIndex(e => e.Celular, "UQ__Usuario__2E4973E724A8E90A")
                    .IsUnique();

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Celular)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("celular");

                entity.Property(e => e.Clave)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.Correo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.Rol)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("rol");
            });

            modelBuilder.Entity<VersionElementoConfiguracion>(entity =>
            {
                entity.HasKey(e => e.IdVersion);

                entity.Property(e => e.IdVersion).HasColumnName("idVersion");

                entity.Property(e => e.Enlace)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("enlace");

                entity.Property(e => e.Fecha)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("fecha");

                entity.Property(e => e.IdSolicitud).HasColumnName("idSolicitud");

                entity.Property(e => e.Version)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("version");

                entity.HasOne(d => d.IdSolicitudNavigation)
                    .WithMany(p => p.VersionElementoConfiguracion)
                    .HasForeignKey(d => d.IdSolicitud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VersionElementoConfiguracion_Solicitud");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
