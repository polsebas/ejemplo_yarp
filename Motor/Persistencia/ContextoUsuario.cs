using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor.Usuario.Persistencia;

public class ContextoUsuario : DbContext
{
    public ContextoUsuario(DbContextOptions<ContextoUsuario> options) : base(options) { }
    
    public DbSet<Entidades.Usuario> Usuarios { get; set; }
    public DbSet<Permisos> Permisos { get; set; }
}
