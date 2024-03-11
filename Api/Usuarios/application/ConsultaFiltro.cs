using Dapper;
using ApiUsuario.Usuarios.domain;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuario.Usuarios.application;

public class ConsultaFiltro
{
    public class UsuarioUnico : IRequest<Usuario>
    {
        public int Id { get; set; }
    }

    public class Manejador : IRequestHandler<UsuarioUnico, Usuario>
    {
        private readonly IConfiguration _configuration;

        public Manejador(IConfiguration configuration) => _configuration = configuration;

        public async Task<Usuario> Handle(UsuarioUnico request, CancellationToken cancellationToken)
        {
            using var db = new SqlConnection(_configuration.GetConnectionString("SqlServer"));
            var usuario = await db.QueryFirstAsync<Usuario>("select * from usuarios where id = @id", new { id = request.Id});
            return usuario ?? throw new Exception("Usuario no encontrado");
        }
    }
}
