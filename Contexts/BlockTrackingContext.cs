using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BlockTime_Tracking.Domains;

#nullable disable

namespace BlockTime_Tracking.Contexts
{
    public partial class BlockTrackingContext : DbContext
    {
        public BlockTrackingContext()
        {
        }

        public BlockTrackingContext(DbContextOptions<BlockTrackingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Equipamento> Equipamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Data Source=db-projetos.c5051cojhtoc.us-east-2.rds.amazonaws.com; initial catalog=BlockTimeTracking; user Id=admin; pwd=04062004;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PRIMARY");

                entity.ToTable("EMPRESAS");

                entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");

                entity.Property(e => e.NomeEmpresa)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("nomeEmpresa");
            });

            modelBuilder.Entity<Equipamento>(entity =>
            {
                entity.HasKey(e => e.IdEquipamento)
                    .HasName("PRIMARY");

                entity.ToTable("EQUIPAMENTOS");

                entity.HasIndex(e => e.IdEmpresa, "idEmpresa");

                entity.HasIndex(e => e.NomeNotebook, "nomeNotebook")
                    .IsUnique();

                entity.Property(e => e.IdEquipamento).HasColumnName("idEquipamento");

                entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");

                entity.Property(e => e.Lat)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("lat");

                entity.Property(e => e.Lng)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("lng");

                entity.Property(e => e.NomeNotebook)
                    .IsRequired()
                    .HasMaxLength(17)
                    .HasColumnName("nomeNotebook")
                    .IsFixedLength(true);

                entity.Property(e => e.UltimaAtt).HasColumnName("ultimaAtt");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Equipamentos)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EQUIPAMENTOS_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
