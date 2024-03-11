using ApiUsuario.Usuarios.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuario.Usuarios.infrastructure;

public class ContextoUsuario : DbContext
{
    public ContextoUsuario(DbContextOptions<ContextoUsuario> options) : base(options) { }
    
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Roles> Roles { get; set; }
}
