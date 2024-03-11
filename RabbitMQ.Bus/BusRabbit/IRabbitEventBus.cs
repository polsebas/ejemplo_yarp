using RabbitMQ.Bus.Comandos;
using RabbitMQ.Bus.Eventos;

namespace RabbitMQ.Bus.BusRabbit;

public interface IRabbitEventBus
{
    Task EnviarComandos<T>(T comando) where T : Comando;
    void Publish<T>(T @evento) where T : Evento;
    void Suscribe<T, TH>() where T : Evento
                           where TH : IEventoManejador<T>;
}
