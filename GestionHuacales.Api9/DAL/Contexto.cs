using GestionHuacales.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionHuacales.Api.DAL;
public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<EntradasHuacales> EntradasHuacales { get; set; }
    public DbSet<EntradasHuacalesDetalle> EntradasHuacalesDetalle { get; set; }
    public DbSet<EntradasHuacalesTipos> EntradasHuacalesTipos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EntradasHuacalesTipos>().HasData(
          new List<EntradasHuacalesTipos>()
          {
              new () {TipoId = 1, Descripción = "Huacal Verde", Existencia = 4},
              new () {TipoId = 2, Descripción = "Huacal Rojo", Existencia = 5},
              new () {TipoId = 3, Descripción = "Huacal Azul", Existencia = 3},
              new () {TipoId = 4, Descripción = "Huacal Amarrillo", Existencia = 6},
              new () {TipoId = 5, Descripción = "Huacal Negro", Existencia = 2}
          }
        );
        base.OnModelCreating(modelBuilder);
    }
}