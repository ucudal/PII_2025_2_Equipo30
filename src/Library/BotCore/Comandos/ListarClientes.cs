using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

public class ListarClientesCommand : IBotCommand
{
    public string Nombre { get; set; } = "Listar clientes";
    public string Descripcion { get; }
    private readonly BotCore _bot;
    private readonly FachadaRegistro _fachada;

    public ListarClientesCommand(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }

    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado)
        {
            contexto.EnviarMensaje("⚠️ Debes iniciar sesión primero.");
            return false;
        }

        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            var clientes = _fachada.ListarClientes(usuario);
            if (clientes.Count == 0)
            {
                contexto.EnviarMensaje("📭 No tienes clientes registrados.");
                return true;
            }

            contexto.EnviarMensaje("👥 Lista de clientes:");
            foreach (var c in clientes)
                contexto.EnviarMensaje($"- {c.Nombre} {c.Apellido} ({c.Email})");
            return true;
        }

        contexto.EnviarMensaje("⚠️ Solo los usuarios pueden listar sus clientes.");
        return false;
    }
}