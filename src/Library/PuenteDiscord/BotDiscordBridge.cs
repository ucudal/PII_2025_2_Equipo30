using Discord.WebSocket;
using Library.BotCore;
using Library.PuenteDiscord;

/// <summary>
/// Puente entre Discord.Net y la lógica interna del bot.
/// Este encarga de recibir los mensajes desde Discord, crear contextos,
/// delegar la ejecución de comandos al núcleo del bot y manejar
/// interacciones activas por usuario.
/// </summary>
public class BotDiscordBridge
{
    /// <summary>
    /// Cliente principal de Discord.Net usado para recibir eventos y enviar mensajes.
    /// </summary>
    private readonly DiscordSocketClient _client;

    /// <summary>
    /// Núcleo lógico del bot, encargado de procesar comandos e interacciones.
    /// </summary>
    private readonly BotCore _core;

    /// <summary>
    /// Inicializa el puente y suscribe el manejador de eventos
    /// para procesar cada mensaje recibido desde Discord.
    /// </summary>
    /// <param name="client">Instancia de <see cref="DiscordSocketClient"/> usada por el bot.</param>
    /// <param name="core">Instancia del núcleo lógico del bot.</param>
    public BotDiscordBridge(DiscordSocketClient client, BotCore core)
    {
        _client = client;
        _core = core;

        _client.MessageReceived += OnMessageReceived;
    }

    /// <summary>
    /// Evento invocado cada vez que llega un mensaje desde Discord.
    /// Maneja:
    /// - Ignorar bots
    /// - Continuar interacciones activas
    /// - Comando "!comandos"
    /// - Ejecución de comandos numéricos
    /// - Respuestas por defecto
    /// </summary>
    /// <param name="msg">Mensaje recibido desde Discord.</param>
    /// <returns>Tarea completada.</returns>
    private Task OnMessageReceived(SocketMessage msg)
    {
        // Ignorar bots
        if (msg.Author.IsBot)
            return Task.CompletedTask;

        // Si el usuario ya está ejecutando un comando interactivo:
        if (ActiveContextManager.TryGetContext(msg.Author.Id, out var ctx))
        {
            ctx.RecibirMensaje(msg.Content);
            return Task.CompletedTask;
        }

        // Comando textual básico
        if (msg.Content == "!comandos")
        {
            msg.Channel.SendMessageAsync(_core.MostrarComandos());
            return Task.CompletedTask;
        }

        // Comando mediante número (solo números)
        if (int.TryParse(msg.Content, out var _))
        {
            var contexto = new DiscordMessageContext(msg.Channel, msg.Author.Id);
            ActiveContextManager.Registrar(msg.Author.Id, contexto);

            // Ejecutar comando en segundo plano para no bloquear el bot
            _ = Task.Run(() =>
            {
                try
                {
                    bool ejecutado = _core.ProcesarOpcion(msg.Content, contexto);
                }
                finally
                {
                    ActiveContextManager.Finalizar(msg.Author.Id);
                }
            });

            return Task.CompletedTask;
        }

        // Respuesta por defecto si no coincide ningún comando
        var contexto2 = new DiscordMessageContext(msg.Channel, msg.Author.Id);
        _core.EnviarMensaje("Solo entiendo números, ingresa un número para ejecutar un comando", contexto2);
        _core.EnviarMensaje(_core.MostrarComandos(), contexto2);

        return Task.CompletedTask;
    }
}
