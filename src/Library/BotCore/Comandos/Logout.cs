using Library.BotCore.Interfaces;

namespace Library.BotCore.Comandos;

/// <summary>
/// Cierra la sesión abierta independientemente de que tipo de usuario este logueado.
/// </summary>
public class LogoutCommand : IBotCommand
{
    public string Nombre { get; set; } = "Cerrar sesión";
    public string Descripcion { get; } = "Cierra la sesión independientemente de quién este logueado";
    private readonly BotCore _bot;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bot"></param>
    public LogoutCommand(BotCore bot)
    {
        _bot = bot;
    }

    /// <summary>
    /// Cierra la sesión de cualquier tipo de usuario logueado.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns>
    /// <c>true</c> si se pudo cerrar la sesión.
    /// <c>false</c> si no existe sesión abierta.
    /// </returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado)
        {
            contexto.EnviarMensaje("⚠️ No hay ninguna sesión iniciada.");
            return false;
        }

        _bot.Sesion.CerrarSesion();
        contexto.EnviarMensaje("👋 Sesión cerrada correctamente.");
        return true;
    }
}