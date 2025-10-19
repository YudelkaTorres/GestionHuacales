using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Api9.DTO;
public class EntradasHuacalesTiposDto
{
    public int TipoId { get; set; }
    public string Descripción { get; set;} = string.Empty;
    public int Existencia { get; set; } 
}
