using RabbitMQ.Bus.BusRabbit;
using RabbitMQ.Bus.EventoQueue;

namespace ApiUsuario.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
    {
        public Task Handle(EmailEventoQueue @event)
        {
            return Task.CompletedTask;
        }
    }
}
