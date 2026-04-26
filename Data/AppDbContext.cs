using System;
using Microsoft.EntityFrameworkCore;
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

        
        modelBuilder.Entity<Categoria>(entity => {
            entity.ToTable("Categorias");
            entity.HasKey(e => e.IdCategoria);
            entity.Property(e => e.NombreCategoria).IsRequired().HasMaxLength(100);
        });

        
        modelBuilder.Entity<Empresa>(entity => {
            entity.ToTable("Empresa");
            entity.HasKey(e => e.IdEmpresa);
            entity.Property(e => e.NombreEmpresa).IsRequired().HasMaxLength(150);
            entity.Property(e => e.TelefonoEmpresa).IsRequired().HasMaxLength(20);
            entity.Property(e => e.CorreoEmpresa).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DireccionEmpresa).IsRequired();
        });

        
        modelBuilder.Entity<Usuario>(entity => {
            entity.ToTable("Usuarios");
            entity.HasKey(e => e.IdUsuarios);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Apellido).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CI).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Celular).IsRequired().HasMaxLength(20);
            
            
            entity.HasIndex(e => e.CI).IsUnique();
        });

        
        modelBuilder.Entity<Pago>(entity => {
            entity.ToTable("Pagos");
            entity.HasKey(e => e.IdPago);
            entity.Property(e => e.MetodoPago).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Monto).IsRequired().HasColumnType("decimal(10,2)");
            entity.Property(e => e.FechaPago).IsRequired();

            entity.HasOne(d => d.Reserva)
                  .WithMany(p => p.Pagos)
                  .HasForeignKey(d => d.IdReserva)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        
        modelBuilder.Entity<Catalogo>(entity => {
            entity.ToTable("Catalogo");
            entity.HasKey(e => e.IdCatalogo);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Precio).IsRequired().HasColumnType("decimal(10,2)");
            entity.Property(e => e.FechaPublicacion).IsRequired();

            
            entity.HasOne(d => d.EmpresaPropietaria)
                  .WithMany(p => p.Productos)
                  .HasForeignKey(d => d.IdEmpresa)
                  .OnDelete(DeleteBehavior.Restrict);

            
            entity.HasOne(d => d.CategoriaAsignada)
                  .WithMany(p => p.ItemsCatalogo)
                  .HasForeignKey(d => d.IdCategoria)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        
        modelBuilder.Entity<Reserva>(entity => {
            entity.ToTable("Reservas");
            entity.HasKey(e => e.IdReserva);
            entity.Property(e => e.FechaReserva).IsRequired();

            
            entity.HasOne(d => d.UsuarioReserva)
                  .WithMany(p => p.MisReservas)
                  .HasForeignKey(d => d.IdReserva);

           
            entity.HasOne(d => d.ProductoReservado)
                  .WithMany() 
                  .HasForeignKey(d => d.IdCatalogo);

        });
    }
}