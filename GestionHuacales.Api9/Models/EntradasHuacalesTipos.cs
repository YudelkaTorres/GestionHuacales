using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Api.Models;
public class EntradasHuacalesTipos
{
    [Key]
    public int TipoId { get; set; }
    [Required(ErrorMessage ="La descripción es obligadoria.")]
    public string Descripción { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage ="La existencia no puede ser negativa.")]
    public int Existencia { get; set; }

    [InverseProperty("EntradaHuacalTipo")]
    public virtual ICollection<EntradasHuacalesDetalle> EntradasHuacalesDetalle { get; set; } = new List<EntradasHuacalesDetalle>();
}
