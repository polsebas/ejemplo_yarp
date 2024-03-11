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

public class Consulta
{
    public class ListaUsuarios : IRequest<List<Usuario>> { }

    public class Manejador : IRequestHandler<ListaUsuarios, List<Usuario>>
    {
        private readonly IConfiguration _configuration;

        public Manejador(IConfiguration configuration) => _configuration = configuration;

        public async Task<List<Usuario>> Handle(ListaUsuarios request, CancellationToken cancellationToken)
        {
            using var db = new SqlConnection(_configuration.GetConnectionString("SqlServer"));
            return (List<Usuario>)await db.QueryAsync<Usuario>("select * from usuarios");
        }
    }
}