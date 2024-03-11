using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Motor.Usuario.Persistencia;
using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Motor.Usuario;

public class Nuevo
{
    public class Ejecuta : IRequest<int> , IValidatableObject
    {
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(FechaNacimiento >= DateTime.Today)
            {
                yield return new ValidationResult("La Fecha de Naciemiento no puede ser mayor o igual al dia de hoy",
                                    new[] { nameof(FechaNacimiento) });
            }
        }
    }

    public class Manejador : IRequestHandler<Ejecuta, int>
    {
        private readonly IConfiguration _configuration;

        public Manejador(IConfiguration configuration) => _configuration = configuration;

        public async Task<int> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var parametros = new DynamicParameters();
            parametros.Add("Nombre", request.Nombre);
            parametros.Add("Apellido", request.Apellido);
            parametros.Add("Password", request.Password);
            parametros.Add("Email", request.Email);
            parametros.Add("FechaNacimiento", request.FechaNacimiento);            
            parametros.Add("mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            parametros.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using var db = new SqlConnection(_configuration.GetConnectionString("SqlServer"));
            await db.ExecuteAsync("InsertarUsuario", parametros, commandType: CommandType.StoredProcedure);
            var mensaje = parametros.Get<string>("mensaje");
            var id = parametros.Get<int?>("id");
            return id != null ? (int)id: throw new Exception("Error al guardar usuario: " + mensaje);
        }
    }
}
