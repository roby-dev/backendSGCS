using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backendSGCS.Models
{
    public partial class dbSGCSContext : DbContext
    {
        private static dbSGCSContext? instance;
        public dbSGCSContext()
        {
        }

        public static dbSGCSContext getContext() {
            if (instance == null) {
                instance = new dbSGCSContext();
                return instance;
            }
            return instance;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=tcp:200.106.124.168,1433;Initial Catalog=dbSGCS;Persist Security Info=True;User ID=roby;Password=Sistemas.123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.HasKey(e => e.IdCargo)
                    .HasName("PK_CARGO");

                entity.Property(e => e.IdCargo).HasColumnName("idCargo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
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

                entity.Property(e => e.IdEntregable).HasColumnName("idEntregable");

                entity.Property(e => e.IdLineaBase).HasColumnName("idLineaBase");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdEntregableNavigation)
                    .WithMany(p => p.ElementoConfiguracion)
                    .HasForeignKey(d => d.IdEntregable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ElementoConfiguracion_fk2");

                entity.HasOne(d => d.IdLineaBaseNavigation)
                    .WithMany(p => p.ElementoConfiguracion)
                    .HasForeignKey(d => d.IdLineaBase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ElementoConfiguracion_fk1");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.ElementoConfiguracion)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ElementoConfiguracion_fk0");
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

                entity.Property(e => e.FechaFin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fechaFin");

                entity.Property(e => e.FechaInicio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fechaInicio");

                entity.Property(e => e.IdFaseMetodologia).HasColumnName("idFaseMetodologia");

                entity.Property(e => e.Nomenclatura)
                    .HasMaxLength(255)
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

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdFaseMetodologia).HasColumnName("idFaseMetodologia");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.HasOne(d => d.IdFaseMetodologiaNavigation)
                    .WithMany(p => p.LineaBase)
                    .HasForeignKey(d => d.IdFaseMetodologia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LineaBase_fk1");

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
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fechaFin");

                entity.Property(e => e.FechaInicio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fechaInicio");

                entity.Property(e => e.IdMetodologia).HasColumnName("idMetodologia");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

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

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdEntregable).HasColumnName("idEntregable");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.Objetivo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("objetivo");

                entity.Property(e => e.Respuesta)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("respuesta");

                entity.Property(e => e.Solicitante)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("solicitante");

                entity.HasOne(d => d.IdEntregableNavigation)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.IdEntregable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Solicitud_fk1");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Solicitud_fk0");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK_USUARIO");

                entity.HasIndex(e => e.Correo, "UQ__Usuario__2A586E0B7665E6C4")
                    .IsUnique();

                entity.HasIndex(e => e.Celular, "UQ__Usuario__2E4973E7399CC44D")
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
