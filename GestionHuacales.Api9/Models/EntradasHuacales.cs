using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Api.Models;
public class EntradasHuacales
{
	[Key]
	public int EntradaId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "No se permiten caracteres especiales")]
    [Required(ErrorMessage = "El cliente es obligatorio")]
    public string NombreCliente { get; set; } = string.Empty;

    [Required]
    [Range(0.0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 1")]
    public int Cantidad { get; set; }

    [Required]
    [Range(0.0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0")]
    public decimal Precio { get; set; }

    [ForeignKey("EntradaHuacalTipo")]
    public int TipoId { get; set; }

    [InverseProperty("EntradaHuacal")]
    public virtual ICollection<EntradasHuacalesDetalle> EntradasHuacalesDetalle{ get; set; } = new List<EntradasHuacalesDetalle>();
}
