using AdminCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AdminCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<AreaEmpresa> AreasEmpresa { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<CategoriaGasto> CategoriasGasto { get; set; }
        public DbSet<SubCategoriaGasto> SubCategoriasGasto { get; set; }
        public DbSet<PresupuestoArea> PresupuestosArea { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empresa>()
                .HasIndex(e => e.Ruc)
                .IsUnique();

            modelBuilder.Entity<Proveedor>()
                .HasIndex(p => new { p.EmpresaId, p.Ruc })
                .IsUnique();

            modelBuilder.Entity<PresupuestoArea>()
                .HasIndex(p => new
                {
                    p.EmpresaId,
                    p.AreaEmpresaId,
                    p.Mes,
                    p.Anio
                })
                .IsUnique();

            modelBuilder.Entity<AreaEmpresa>()
                .HasOne(a => a.Empresa)
                .WithMany(e => e.Areas)
                .HasForeignKey(a => a.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proveedor>()
                .HasOne(p => p.Empresa)
                .WithMany(e => e.Proveedores)
                .HasForeignKey(p => p.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubCategoriaGasto>()
                .HasOne(s => s.CategoriaGasto)
                .WithMany(c => c.SubCategorias)
                .HasForeignKey(s => s.CategoriaGastoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PresupuestoArea>()
                .HasOne(p => p.Empresa)
                .WithMany(e => e.PresupuestosArea)
                .HasForeignKey(p => p.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PresupuestoArea>()
                .HasOne(p => p.AreaEmpresa)
                .WithMany(a => a.PresupuestosArea)
                .HasForeignKey(p => p.AreaEmpresaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
