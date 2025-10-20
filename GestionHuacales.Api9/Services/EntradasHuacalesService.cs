using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GestionHuacales.Api.DAL;
using GestionHuacales.Api.Models;
using GestionHuacales.Api.DTO;
using GestionHuacales.Api9.DTO;

namespace GestionHuacales.Api.Services;
public class EntradasHuacalesService
{
    public IDbContextFactory<Contexto> DbFactory { get; }

    public EntradasHuacalesService(IDbContextFactory<Contexto> dbFactory)
    {
        DbFactory = dbFactory;
    }
    public async Task<EntradasHuacalesDto[]> Listar(Expression<Func<EntradasHuacales, bool>> criterio)
    {
       await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .Where(criterio)
            .Include(e => e.EntradasHuacalesDetalle)
            .Select(h => new EntradasHuacalesDto
            {
                NombreCliente = h.NombreCliente,
                Huacales = h.EntradasHuacalesDetalle
                .Select(d  => new EntradasHuacalesDetalleDto
                {
                    TipoId = d.TipoId,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio
                }).ToArray()
            })
            .ToArrayAsync();
    }
    public async Task<bool> Guardar(EntradasHuacales entrada)
    {
        if (!await Existe(entrada.EntradaId))
            return await Insertar(entrada);
        else
            return await Modificar(entrada);
    }
    public async Task<bool> Existe(int entradaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .AnyAsync(e => e.EntradaId == entradaId);
    }
    public async Task<EntradasHuacales?> Buscar(int entradaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .Include(e => e.EntradasHuacalesDetalle)
            .ThenInclude(d => d.EntradaHuacalTipo)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntradaId == entradaId);
    }
    private async Task AfectarExistencia(EntradasHuacales entrada, TipoOperacion tipoOperacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var entradashuacalesdetalle = await contexto.EntradasHuacalesDetalle
            .Where(d => d.EntradaId == entrada.EntradaId)
            .ToListAsync();

        foreach (var entradahuacaldetalle in entradashuacalesdetalle)
        {
            var entradashuacalestipo = await contexto.EntradasHuacalesTipos
                .SingleOrDefaultAsync(t => t.TipoId == entradahuacaldetalle.TipoId);

            if (entradashuacalestipo == null)
                continue;

            if (tipoOperacion == TipoOperacion.Suma)
                entradashuacalestipo.Existencia += entradahuacaldetalle.Cantidad;
            else
                entradashuacalestipo.Existencia -= entradahuacaldetalle.Cantidad;
        }
        await contexto.SaveChangesAsync();
    }
    private async Task<bool> Insertar(EntradasHuacales entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        contexto.EntradasHuacales.Add(entrada);
        await contexto.SaveChangesAsync();

        await AfectarExistencia(entrada, TipoOperacion.Suma);
        return true;
    }   
    private async Task<bool> Modificar(EntradasHuacales entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var anterior = await contexto.EntradasHuacales
            .Include(e => e.EntradasHuacalesDetalle)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntradaId == entrada.EntradaId);

        if (anterior != null)
            await AfectarExistencia(anterior, TipoOperacion.Resta);

        contexto.Update(entrada);
        await contexto.SaveChangesAsync();

        await AfectarExistencia(entrada, TipoOperacion.Suma);
        return true;
    }
    public async Task<bool> Eliminar(int entradaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var entrada = await contexto.EntradasHuacales
            .Include(e => e.EntradasHuacalesDetalle)
            .FirstOrDefaultAsync(e => e.EntradaId == entradaId);

        if (entrada == null)
            return false;

        await AfectarExistencia(entrada, TipoOperacion.Resta);

        contexto.EntradasHuacalesDetalle.RemoveRange(entrada.EntradasHuacalesDetalle);
        contexto.EntradasHuacales.Remove(entrada);

        await contexto.SaveChangesAsync();
        return true;
    }
    public async Task<EntradasHuacalesTiposDto[]> ListarTipos()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacalesTipos
            .Where(t => t.TipoId > 0)
            .Select(t => new EntradasHuacalesTiposDto
            {
                TipoId = t.TipoId,
                Descripción = t.Descripción,
                Existencia = t.Existencia
            }).ToArrayAsync();
    }
}
public enum TipoOperacion
{
    Suma = 1,
    Resta = 2
}