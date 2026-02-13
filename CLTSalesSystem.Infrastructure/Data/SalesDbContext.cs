using CLTSalesSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CLTSalesSystem.Infrastructure.Data
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names for Oracle compatibility
            modelBuilder.Entity<Cliente>().ToTable("CLIENTES");
            modelBuilder.Entity<Producto>().ToTable("PRODUCTOS");
            modelBuilder.Entity<Venta>().ToTable("VENTAS");
            modelBuilder.Entity<DetalleVenta>().ToTable("DETALLES_VENTA");
            modelBuilder.Entity<Usuario>().ToTable("USUARIOS");

            // Configure Key/Relationships if needed beyond conventions
            
            // Venta -> DetalleVenta relationship
            modelBuilder.Entity<Venta>()
                .HasMany(v => v.Detalles)
                .WithOne()
                .HasForeignKey(d => d.VentaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Additional configurations (Precision for decimals is important for Oracle)
            modelBuilder.Entity<Venta>()
                .Property(v => v.Total)
                .HasColumnType("NUMBER(18,2)");

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("NUMBER(18,2)");

            modelBuilder.Entity<DetalleVenta>()
                .Property(d => d.PrecioUnitario)
                .HasColumnType("NUMBER(18,2)");

             modelBuilder.Entity<DetalleVenta>()
                .Property(d => d.Subtotal)
                .HasColumnType("NUMBER(18,2)");
        }
    }
}
