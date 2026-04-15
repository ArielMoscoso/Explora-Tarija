using System;
using Microsoft.EntityFrameworkCore;
// Asegúrate de que el namespace coincida con donde guardaste tus clases
using ExploraTarija.Entidades; 

namespace ExploraTarija.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pago> Pagos { get; set; }
    public DbSet<Catalogo> Catalogos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Configuración Tabla: Categoría ---
        modelBuilder.Entity<Categoria>(entity => {
            entity.ToTable("Categorias");
            entity.HasKey(e => e.IdCategoria);
            entity.Property(e => e.NombreCategoria).IsRequired().HasMaxLength(100);
        });

        // --- Configuración Tabla: Empresa ---
        modelBuilder.Entity<Empresa>(entity => {
            entity.ToTable("Empresa");
            entity.HasKey(e => e.IdEmpresa);
            entity.Property(e => e.NombreEmpresa).IsRequired().HasMaxLength(150);
            entity.Property(e => e.TelefonoEmpresa).IsRequired().HasMaxLength(20);
            entity.Property(e => e.CorreoEmpresa).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DireccionEmpresa).IsRequired();
        });

        // --- Configuración Tabla: Usuarios ---
        modelBuilder.Entity<Usuario>(entity => {
            entity.ToTable("Usuarios");
            entity.HasKey(e => e.IdUsuarios);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CI).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Celular).IsRequired().HasMaxLength(20);
            
            // Si quieres que el CI sea único en la BD:
            entity.HasIndex(e => e.CI).IsUnique();
        });

        // --- Configuración Tabla: Pago ---
        modelBuilder.Entity<Pago>(entity => {
            entity.ToTable("Pago");
            entity.HasKey(e => e.IdPago);
            entity.Property(e => e.MetodoPago).IsRequired().HasMaxLength(50);
            entity.Property(e => e.FechaPago).IsRequired();
        });

        // --- Configuración Tabla: Catálogo ---
        modelBuilder.Entity<Catalogo>(entity => {
            entity.ToTable("Catalogo");
            entity.HasKey(e => e.IdCatalogo);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Precio).IsRequired().HasColumnType("decimal(10,2)");
            entity.Property(e => e.FechaPublicacion).IsRequired();

            // Relación con Empresa (1:N)
            entity.HasOne(d => d.EmpresaPropietaria)
                  .WithMany(p => p.Productos)
                  .HasForeignKey(d => d.IdEmpresa)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relación con Categoría (1:N)
            entity.HasOne(d => d.CategoriaAsignada)
                  .WithMany(p => p.ItemsCatalogo)
                  .HasForeignKey(d => d.IdCategoria)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // --- Configuración Tabla: Reservas ---
        modelBuilder.Entity<Reserva>(entity => {
            entity.ToTable("Reservas");
            entity.HasKey(e => e.IdReservas);
            entity.Property(e => e.FechaReserva).IsRequired();

            // Relación con Usuario
            entity.HasOne(d => d.UsuarioReserva)
                  .WithMany(p => p.MisReservas)
                  .HasForeignKey(d => d.IdUsuario);

            // Relación con Catalogo
            entity.HasOne(d => d.ProductoReservado)
                  .WithMany() // Si no pusiste lista de reservas en Catalogo, se deja vacío
                  .HasForeignKey(d => d.IdCatalogo);

            // Relación con Pago
            entity.HasOne(d => d.DetallePago)
                  .WithMany()
                  .HasForeignKey(d => d.IdPago);
        });
    }
}