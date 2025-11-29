using Discord;
using Library.BotCore.Interfaces;
using System.Threading.Tasks;

namespace Library.PuenteDiscord
{
    public class DiscordMessageContext : IMessageContext
    {
        private readonly IMessageChannel _channel;
        public ulong UserId { get; }

        private TaskCompletionSource<string> _waitingResponse;

        public DiscordMessageContext(IMessageChannel channel, ulong userId)
        {
            _channel = channel;
            UserId = userId;
        }

        public string UsuarioId { get; }
        public string UsuarioNombre { get; }

        public void EnviarMensaje(string msg)
        {
            _channel.SendMessageAsync(msg);
        }

        public string EsperarRespuesta()
        {
            // Evitar sobrescribir si ya se está esperando una respuesta
            if (_waitingResponse == null || _waitingResponse.Task.IsCompleted)
                _waitingResponse = new TaskCompletionSource<string>();

            return _waitingResponse.Task.Result;
        }

        // Llamado desde el bot cuando llegue un mensaje del usuario
        public void RecibirMensaje(string msg)
        {
            _waitingResponse?.TrySetResult(msg);
        }
    }
}