using Microsoft.EntityFrameworkCore;
using Negocios_API.Models;

namespace Negocios_API.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Business> Businesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>().HasData(
                new Owner
                {
                    Id = 1,
                    NombrePropietario = "Joseph Enmanuel Pineda Aguilera",
                    Correo = "josephpineda1210@gmail.com",
                    Clave = "abcd1234",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
            );

            modelBuilder.Entity<Business>().HasData(
                new Business
                {
                    Id = 1,
                    OwnerId = 1,
                    NombreNegocio = "Capri",
                    Direccion = "Linda Vista",
                    RUC = 123456789,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                },
                new Business
                {
                    Id = 2,
                    OwnerId = 1,
                    NombreNegocio = "Serta",
                    Direccion = "Bolonia",
                    RUC = 11223344,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                });
        }
    }
}
