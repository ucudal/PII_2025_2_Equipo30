using System.Data;
using Discord;
using Discord.WebSocket;
using Library.BotCore;
using Library.BotCore.Comandos;
using Library.Fachadas;

class Program
{
    static async Task Main()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents =
                GatewayIntents.Guilds |
                GatewayIntents.GuildMessages |
                GatewayIntents.DirectMessages |
                GatewayIntents.MessageContent
        };
        var client = new DiscordSocketClient(config);
        var botCore = new BotCore();
        var fachada = new FachadaRegistro();

        // Registrar tus comandos:
        botCore.RegistrarComando(new LoginCommand(botCore, fachada));
        botCore.RegistrarComando(new LogoutCommand(botCore));
        botCore.RegistrarComando(new CrearUsuarioAdmin(botCore, fachada)); 
        botCore.RegistrarComando(new AgregarEtiquetaClienteUsuario(botCore, fachada));
        botCore.RegistrarComando(new AsignarClienteVendedor(botCore, fachada));
        botCore.RegistrarComando(new BuscarClienteUsuario(botCore, fachada));
        botCore.RegistrarComando(new ContactoPendienteClienteUsuario(botCore, fachada));
        botCore.RegistrarComando(new CrearClienteUsuario(botCore, fachada));
        botCore.RegistrarComando(new CrearEtiquetaUsuario(botCore, fachada));
        botCore.RegistrarComando(new CrearUsuarioAdmin(botCore, fachada));
        botCore.RegistrarComando(new CrearVendedorUsuario(botCore, fachada));
        botCore.RegistrarComando(new EliminarClienteUsuario(botCore, fachada));
        botCore.RegistrarComando(new EliminarUsuarioAdmin(botCore, fachada));
        botCore.RegistrarComando(new InteraccionesClienteUsuario(fachada, botCore));
        botCore.RegistrarComando(new ListarClientesCommand(botCore, fachada));
        botCore.RegistrarComando(new ListarClientesVendedor(botCore));
        botCore.RegistrarComando(new ModificarClienteUsuario(fachada, botCore));
        botCore.RegistrarComando(new PromedioVentasUsuario(botCore, fachada));
        botCore.RegistrarComando(new RegistrarCotizacionClienteUsuario(fachada, botCore));
        botCore.RegistrarComando(new RegistrarVentaClienteUsuario(botCore, fachada));
        botCore.RegistrarComando(new RegistroComunicacionUsuario(botCore, fachada));
        botCore.RegistrarComando(new SuspenderUsuarioAdmin(botCore, fachada));
        botCore.RegistrarComando(new UltimaInteraccionClienteUsuario(botCore, fachada));
        botCore.RegistrarComando(new VerPanelResumenUsuario(botCore, fachada));
        //Se pueden agregar más comandos si se quiere, se van a listar en el orden de agregado.

        // Inicializar el puente
        new BotDiscordBridge(client, botCore);

        await client.LoginAsync(TokenType.Bot, "TOKEN ACÁ");
        await client.StartAsync();

        await Task.Delay(-1);
    }
}