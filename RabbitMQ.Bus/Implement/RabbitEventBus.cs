using MediatR;
using RabbitMQ.Bus.BusRabbit;
using RabbitMQ.Bus.Comandos;
using RabbitMQ.Bus.Eventos;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Bus.Implement;

public class RabbitEventBus : IRabbitEventBus
{
    private readonly IMediator _mediator;
    private readonly Dictionary<string, List<Type>> _manejadores;
    private readonly List<Type> _eventosTipos;

    public RabbitEventBus(IMediator mediator)
    {
        _mediator = mediator;
        _manejadores = new Dictionary<string, List<Type>>();
        _eventosTipos = new List<Type>();
    }

    public Task EnviarComandos<T>(T comando) where T : Comando
    {
        return _mediator.Send(comando);
    }

    public void Publish<T>(T evento) where T : Evento
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        var eventName = evento.GetType().Name;
        channel.QueueDeclare(eventName, false, false, false, null);
        var message = JsonConvert.SerializeObject(evento);
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish("", eventName, null, body);
    }

    public void Suscribe<T, TH>()
        where T : Evento
        where TH : IEventoManejador<T>
    {
        var eventoNombre = typeof(T).Name;
        var manejadorEventoTipo = typeof(TH);
        if (!_eventosTipos.Contains(typeof(T)))
        {
            _eventosTipos.Add(typeof(T));
        }
        if (!_manejadores.ContainsKey(eventoNombre))
        {
            _manejadores.Add(eventoNombre, new List<Type>());
        }
        if (_manejadores[eventoNombre].Any(x => x.GetType() == manejadorEventoTipo))
        {
            throw new ArgumentException($"El manejado {manejadorEventoTipo.Name} fue registrado anteriormente por {eventoNombre}");
        }
        _manejadores[eventoNombre].Add(manejadorEventoTipo);
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            DispatchConsumersAsync = true
        };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        channel.QueueDeclare(eventoNombre, false, false, false, null);
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += Consumer_Received;
        channel.BasicConsume(eventoNombre, true, consumer);
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        var nombreEvento = e.RoutingKey;
        var message = Encoding.UTF8.GetString(e.Body.ToArray());
        try
        {
            if (_manejadores.ContainsKey(nombreEvento))
            {
                var subscriptions = _manejadores[nombreEvento];
                foreach (var subscription in subscriptions)
                {
                    var manejador = Activator.CreateInstance(subscription);
                    if (manejador == null) continue;
                    var tipoEvento = _eventosTipos.Single(x => x.Name == nombreEvento);
                    var eventoDS = JsonConvert.DeserializeObject(message, tipoEvento);
                    var concretoTipo = typeof(IEventoManejador<>).MakeGenericType(tipoEvento);
                    await (Task)concretoTipo.GetMethod("Handle").Invoke(manejador, new object[] { eventoDS });
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}
