using ApiUsuario.Usuarios.domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuario.Usuarios.application
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuariosController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<int>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return await _mediator.Send(new Consulta.ListaUsuarios());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            return await _mediator.Send(new ConsultaFiltro.UsuarioUnico { Id = id });
        }

        [Route("ExisteEmail/{email}")]
        [AcceptVerbs("GET", "POST")]
        public async Task<ActionResult<bool>> ExisteEmail(string email)
        {
            return await _mediator.Send(new ExisteEmail.UsuarioEmail { Email = email });
        }
    }
}

