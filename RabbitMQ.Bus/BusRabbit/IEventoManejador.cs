using RabbitMQ.Bus.Eventos;

namespace RabbitMQ.Bus.BusRabbit;

public interface IEventoManejador<in TEvent> : IEventoManejador where TEvent : Evento
{
    Task Handle(TEvent @event);
}

public interface IEventoManejador { }
