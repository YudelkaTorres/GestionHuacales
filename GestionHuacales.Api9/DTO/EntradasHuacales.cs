using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Api.DTO;
public class EntradasHuacalesDto
{
    public string NombreCliente { get; set; } = string.Empty;
    public EntradasHuacalesDetalleDto[] Huacales { get; set; } = [];
}