namespace RabbitMQ.Bus.Eventos;

public abstract class Evento
{
    public DateTime Timestamp { get; protected set; }

    protected Evento()
    {
        Timestamp = DateTime.Now;
    }
}
