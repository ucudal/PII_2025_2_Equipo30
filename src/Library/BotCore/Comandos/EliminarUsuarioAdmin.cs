using Library.BotCore.Interfaces;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

/// <summary>
/// Permite eliminar un usuario siendo administrador.
/// </summary>
public class EliminarUsuarioAdmin : IBotCommand
{
    public string Nombre { get; set; } = "Eliminar usuario";
    public string Descripcion { get; } = "Permite eliminar un usuario por Id siendo administrador.";
    private readonly BotCore _bot;
    private readonly FachadaRegistro _fachada;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bot"></param>
    /// <param name="fachada"></param>
    public EliminarUsuarioAdmin(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    /// <summary>
    /// Ejecuta el comando que Elimina al Usuario por su Id.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Administrador")
        {
            contexto.EnviarMensaje("❌ Solo un administrador puede eliminar usuarios.");
            return false;
        }

        contexto.EnviarMensaje("Ingrese Id del usuario a eliminar: ");
        string respuesta = contexto.EsperarRespuesta();

        if (!int.TryParse(respuesta, out int usuarioId))
        {
            contexto.EnviarMensaje("⚠️ El Id ingresado no es válido.");
            return false;
        }

        bool eliminado = _fachada.EliminarUsuarioPorId(usuarioId);

        if (eliminado)
        {
            contexto.EnviarMensaje($"✅ Usuario con Id {usuarioId} eliminado correctamente.");
            return true;
        }
        else
        {
            contexto.EnviarMensaje($"❌ No se encontró el usuario con Id {usuarioId}.");
            return false;
        }
    }
}