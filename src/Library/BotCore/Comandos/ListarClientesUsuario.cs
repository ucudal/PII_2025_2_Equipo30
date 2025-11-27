using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

/// <summary>
/// Comando que lista los clientes asignados al Usuario.
/// </summary>
public class ListarClientesCommand : IBotCommand
{
    public string Nombre { get; set; } = "Listar clientes";
    public string Descripcion { get; }
    private readonly BotCore _bot;
    private readonly FachadaRegistro _fachada;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bot">Instancia de BotCore.</param>
    /// <param name="fachada">Instancia de FachadaRegistro.</param>
    public ListarClientesCommand(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }

    /// <summary>
    /// Ejecuta el listado de clientes asignados al usuario que ejecuta el comando.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado)
        {
            contexto.EnviarMensaje("⚠️ Debes iniciar sesión primero.");
            return false;
        }

        // Se usa is Usuario usuario para poder referenciar la instancia del usuario logueado en los comandos de fachada.
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
                contexto.EnviarMensaje($"- {c.Nombre} {c.Apellido} ({c.Email} {c.Id})");
            return true;
        }

        contexto.EnviarMensaje("⚠️ Solo los usuarios pueden listar sus clientes.");
        return false;
    }
}