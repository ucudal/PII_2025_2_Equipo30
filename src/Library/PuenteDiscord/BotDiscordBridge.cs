using Discord.WebSocket;
using Library.BotCore;
using Library.PuenteDiscord;

public class BotDiscordBridge
{
    private readonly DiscordSocketClient _client;
    private readonly BotCore _core;

    public BotDiscordBridge(DiscordSocketClient client, BotCore core)
    {
        _client = client;
        _core = core;

        _client.MessageReceived += OnMessageReceived;
    }

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

        // Si escribe "!comandos" (puedes cambiar esto por lo que quieras)
        if (msg.Content == "!comandos")
        {
            msg.Channel.SendMessageAsync(_core.MostrarComandos());
            return Task.CompletedTask;
        }

        // Intentar ejecutar un comando mediante número (solo números)
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

        var contexto2 = new DiscordMessageContext(msg.Channel, msg.Author.Id);
        _core.EnviarMensaje("Solo entiendo números, ingresa un número para ejecutar un comando", contexto2);
        _core.EnviarMensaje(_core.MostrarComandos(), contexto2);

        // Si no coincide nada…
        return Task.CompletedTask;
    }
}