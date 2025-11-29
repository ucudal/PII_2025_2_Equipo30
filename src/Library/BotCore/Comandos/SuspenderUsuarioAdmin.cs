using Library.BotCore.Interfaces;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

/// <summary>
/// Permite suspender un usuario siendo administrador.
/// </summary>
public class SuspenderUsuarioAdmin : IBotCommand
{
    public string Nombre { get; set; } = "Suspender usuario";
    public string Descripcion { get; } = "Permite suspender un usuario siendo administrador.";
    private readonly BotCore _bot;
    private readonly FachadaRegistro _fachada;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bot"></param>
    /// <param name="fachada"></param>
    public SuspenderUsuarioAdmin(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    /// <summary>
    /// Ejecuta el comando que suspende el usuario por Id.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Administrador")
        {
            contexto.EnviarMensaje("❌ Solo un administrador puede suspender usuarios.");
            return false;
        }

        contexto.EnviarMensaje("Ingrese Id del usuario: ");
        string respuesta = contexto.EsperarRespuesta();

        if (!int.TryParse(respuesta, out int usuarioId))
        {
            contexto.EnviarMensaje("⚠️ El Id ingresado no es válido.");
            return false;
        }

        bool suspendido = _fachada.SuspenderUsuario(usuarioId); // <-- Necesitas implementar este método en FachadaRegistro

        if (suspendido)
        {
            contexto.EnviarMensaje($"✅ Usuario con Id {usuarioId} suspendido correctamente.");
            return true;
        }
        else
        {
            contexto.EnviarMensaje($"❌ No se encontró el usuario con Id {usuarioId}.");
            return false;
        }
    }
}