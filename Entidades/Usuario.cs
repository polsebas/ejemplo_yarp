using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades;

public class Usuario
{
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; } = string.Empty;
    [Required]
    public string Apellido { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Date)]
    public DateTime FechaNacimiento { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
    public DateTime? UltimoAcceso { get; set; }
    public string? Token { get;set; }
    public List<Roles> Roles { get; set; } = new();
}
