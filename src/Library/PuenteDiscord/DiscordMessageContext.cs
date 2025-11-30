using Discord;
using Library.BotCore.Interfaces;
using System.Threading.Tasks;

namespace Library.PuenteDiscord
{
    /// <summary>
/// Representa un contexto de interacción para un usuario en Discord,
/// permitiendo enviar mensajes y esperar respuestas de forma asincrónica.
/// </summary>
public class DiscordMessageContext : IMessageContext
{
    /// <summary>
    /// Canal de Discord donde se envían y reciben mensajes.
    /// </summary>
    private readonly IMessageChannel _channel;

    /// <summary>
    /// ID del usuario asociado a este contexto.
    /// </summary>
    public ulong UserId { get; }

    /// <summary>
    /// TaskCompletionSource se utiliza para esperar una respuesta del usuario.
    /// </summary>
    private TaskCompletionSource<string> _waitingResponse;

    /// <summary>
    /// Crea una nueva instancia del contexto de mensaje para un usuario específico.
    /// </summary>
    /// <param name="channel">Canal de Discord donde se enviarán los mensajes.</param>
    /// <param name="userId">ID del usuario asociado al contexto.</param>
    public DiscordMessageContext(IMessageChannel channel, ulong userId)
    {
        _channel = channel;
        UserId = userId;
    }

    /// <summary>
    /// ID del usuario en formato string (implementación de <see cref="IMessageContext"/>).
    /// Actualmente no está inicializada.
    /// </summary>
    public string UsuarioId { get; }

    /// <summary>
    /// Nombre del usuario (implementación de <see cref="IMessageContext"/>).
    /// Actualmente no está inicializada.
    /// </summary>
    public string UsuarioNombre { get; }

    /// <summary>
    /// Envía un mensaje al usuario en el canal asociado a este contexto.
    /// </summary>
    /// <param name="msg">Contenido del mensaje a enviar.</param>
    public void EnviarMensaje(string msg)
    {
        _channel.SendMessageAsync(msg);
    }

    /// <summary>
    /// Bloquea la ejecución esperando una respuesta del usuario.
    /// Este método no debería usarse en entornos asincrónicos para evitar deadlocks.
    /// </summary>
    /// <returns>El mensaje recibido del usuario.</returns>
    public string EsperarRespuesta()
    {
        // Evitar sobrescribir si ya se está esperando una respuesta
        if (_waitingResponse == null || _waitingResponse.Task.IsCompleted)
            _waitingResponse = new TaskCompletionSource<string>();

        return _waitingResponse.Task.Result;
    }

    /// <summary>
    /// Método llamado por el bot cuando recibe un mensaje del usuario.
    /// Libera la espera estableciendo el resultado de la tarea.
    /// </summary>
    /// <param name="msg">Mensaje recibido por el usuario.</param>
    public void RecibirMensaje(string msg)
    {
        _waitingResponse?.TrySetResult(msg);
    }
}

}