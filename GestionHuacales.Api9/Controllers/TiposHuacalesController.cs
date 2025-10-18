using GestionHuacales.Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionHuacales.Api9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposHuacalesController (EntradasHuacalesService entradasHuacalesService): ControllerBase
    {
        // GET: api/<TiposHuacalesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TiposHuacalesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TiposHuacalesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TiposHuacalesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TiposHuacalesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
