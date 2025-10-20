using GestionHuacales.Api.DTO;
using GestionHuacales.Api.Models;
using GestionHuacales.Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionHuacales.Api9.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntradasHuacalesController (EntradasHuacalesService entradasHuacalesService): ControllerBase
{
    // GET: api/<EntradasHuacalesController>
    [HttpGet]
    public async Task <EntradasHuacalesDto[]> Get()
    {
        return await entradasHuacalesService.Listar(h => true);
    }

    // GET api/<EntradasHuacalesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EntradasHuacalesDetalleDto>> Get(int id)
    {
        var entrada = await entradasHuacalesService.Buscar(id);
        if (entrada == null)
            return NotFound();

        var entradasHuacalesDto = new EntradasHuacalesDto
        {
            NombreCliente = entrada.NombreCliente,
            Huacales = entrada.EntradasHuacalesDetalle.Select(d => new EntradasHuacalesDetalleDto
            {
                TipoId = d.TipoId,
                Cantidad = d.Cantidad,
                Precio = d.Precio
            }).ToArray()
        };

        return Ok(entradasHuacalesDto);
    }

    // POST api/<EntradasHuacalesController>
    [HttpPost]
    public async Task Post([FromBody] EntradasHuacalesDto entradaHuacales)
    {
        var huacales = new EntradasHuacales
        {
            Fecha = DateTime.Now,
            NombreCliente = entradaHuacales.NombreCliente,
            EntradasHuacalesDetalle = entradaHuacales.Huacales.Select(h => new EntradasHuacalesDetalle
            { 
                TipoId = h.TipoId,
                Cantidad = h.Cantidad,
                Precio = h.Precio 
                }).ToArray()
        };
       await entradasHuacalesService.Guardar(huacales);
    }

    // PUT api/<EntradasHuacalesController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] EntradasHuacalesDto entradasHuacalesDto)
    {
        var huacales = new EntradasHuacales
        {
            EntradaId = id,
            Fecha = DateTime.Now,
            NombreCliente = entradasHuacalesDto.NombreCliente,
            EntradasHuacalesDetalle = entradasHuacalesDto.Huacales.Select(h => new EntradasHuacalesDetalle
            {
                TipoId = h.TipoId,
                Cantidad = h.Cantidad,
                Precio = h.Precio,
            }).ToArray()
        };

        await entradasHuacalesService.Guardar(huacales);
        return Ok();
    }

    // DELETE api/<EntradasHuacalesController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var eliminado = await entradasHuacalesService.Eliminar(id);
        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}
