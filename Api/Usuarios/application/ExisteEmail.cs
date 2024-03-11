using Dapper;
using ApiUsuario.Usuarios.domain;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuario.Usuarios.application;

public class ExisteEmail
{
    public class UsuarioEmail : IRequest<bool>
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }

    public class Manejador : IRequestHandler<UsuarioEmail, bool>
    {
        private readonly IConfiguration _configuration;

        public Manejador(IConfiguration configuration) => _configuration = configuration;

        public async Task<bool> Handle(UsuarioEmail request, CancellationToken cancellationToken)
        {
            using var db = new SqlConnection(_configuration.GetConnectionString("SqlServer"));
            var usuario = await db.QueryFirstOrDefaultAsync<Usuario>("select * from usuarios where email = @Email", new { Email = request.Email });
            return usuario != null;
        }
    }
}
