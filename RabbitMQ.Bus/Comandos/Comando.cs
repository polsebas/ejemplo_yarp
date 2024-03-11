using RabbitMQ.Bus.Eventos;

namespace RabbitMQ.Bus.Comandos;

public abstract class Comando : Message
{
    public DateTime Timespan { get; protected set; }
    protected Comando()
    {
        Timespan = DateTime.Now;
    }
}
