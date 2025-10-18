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
    public string Get(int id)
    {
        return "value";
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
                Precio = h.Precio, 
                }).ToArray()
        };
       await entradasHuacalesService.Guardar(huacales);
    }

    // PUT api/<EntradasHuacalesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<EntradasHuacalesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
