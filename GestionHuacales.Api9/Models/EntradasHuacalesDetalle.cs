using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Api.Models;

public class EntradasHuacalesDetalle
{
    [Key]
    public int DetalleId { get; set; }
    public int EntradaId { get; set; }
    public int TipoId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    [ForeignKey("EntradaId")]
    [InverseProperty("EntradasHuacalesDetalle")]
    public virtual EntradasHuacales EntradaHuacal { get; set; } = null!;

    [ForeignKey("TipoId")]
    [InverseProperty("EntradasHuacalesDetalle")]
    public virtual EntradasHuacalesTipos EntradaHuacalTipo { get; set; } = null!;
    
}
