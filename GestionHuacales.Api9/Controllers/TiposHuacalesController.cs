using GestionHuacales.Api.Models;
using GestionHuacales.Api.Services;
using GestionHuacales.Api9.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionHuacales.Api9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposHuacalesController (EntradasHuacalesService entradasHuacalesService): ControllerBase
    {
        // GET: api/<TiposHuacalesController>
        [HttpGet]
        public async Task<EntradasHuacalesTiposDto[]> Get()
        {
            return await entradasHuacalesService.ListarTipos();
        }

        // GET api/<TiposHuacalesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntradasHuacalesTiposDto>> Get(int id)
        {
            var tipos = await entradasHuacalesService.ListarTipos();
            var tipo = tipos.FirstOrDefault(t => t.TipoId == id);
            if (tipo == null) return NotFound();
            return tipo;
        }

        // POST api/<TiposHuacalesController>
        [HttpPost]
        public async Task<ActionResult<EntradasHuacalesTiposDto>> Post([FromBody] EntradasHuacalesTiposDto entradasHuacalesTiposDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await using var contexto = await entradasHuacalesService.DbFactory.CreateDbContextAsync();

            var tipo = new EntradasHuacalesTipos
            {
                Descripción = entradasHuacalesTiposDto.Descripción,
                Existencia = entradasHuacalesTiposDto.Existencia
            };

            contexto.EntradasHuacalesTipos.Add(tipo);
            await contexto.SaveChangesAsync();

            entradasHuacalesTiposDto.TipoId = tipo.TipoId;
            return CreatedAtAction(nameof(Get), new {id = entradasHuacalesTiposDto.TipoId},entradasHuacalesTiposDto);
        }

        // PUT api/<TiposHuacalesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EntradasHuacalesTiposDto entradasHuacalesTiposDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await using var contexto = await entradasHuacalesService.DbFactory.CreateDbContextAsync();

            var tipo = await contexto.EntradasHuacalesTipos.FindAsync(id);
            if (tipo == null) return NotFound();

            tipo.Descripción = entradasHuacalesTiposDto.Descripción;
            tipo.Existencia = entradasHuacalesTiposDto.Existencia;

            contexto.Update(tipo);
            await contexto.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<TiposHuacalesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await using var contexto = await entradasHuacalesService.DbFactory.CreateDbContextAsync();
            var tipo = await contexto.EntradasHuacalesTipos.FindAsync(id);
            if (tipo == null) return NotFound();

            var detalles = await contexto.EntradasHuacalesDetalle
                .Where(d => d.TipoId == id)
                .ToListAsync();

            if (detalles.Any()) 
            {
                contexto.EntradasHuacalesDetalle.RemoveRange(detalles);
            }

            contexto.EntradasHuacalesTipos.Remove(tipo);
            await contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
