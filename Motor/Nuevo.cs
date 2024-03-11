using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor.Usuario;

public class Nuevo
{
    public class Ejecuta : IRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
        public Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
