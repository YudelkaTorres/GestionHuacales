using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Api.DTO;
public class EntradasHuacalesDetalleDto
{
    public int TipoId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
}
