using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;
/// <summary>
/// Muestra clientes totales, interacciones recientes y reuniones próximas.
/// </summary>
public class VerPanelResumenUsuario : IBotCommand
{
    public string Nombre { get; } = "Ver panel resumen";
    public string Descripcion { get; } = "Muestra clientes totales, interacciones recientes y reuniones próximas.";
    private readonly BotCore _bot;
    private readonly FachadaRegistro _fachada;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bot"></param>
    /// <param name="fachada"></param>
    public VerPanelResumenUsuario(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    /// <summary>
    /// Ejecuta un comando que Genera un resumen del usuario.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado)
        {
            contexto.EnviarMensaje("❌ Debe iniciar sesión para ver el panel.");
            return false;
        }

        var usuario = _bot.Sesion.UsuarioActual as Usuario;
        if (usuario == null)
        {
            contexto.EnviarMensaje("⚠️ La sesión actual no corresponde a un usuario.");
            return false;
        }

        var panel = _fachada.ObtenerPanelResumen(usuario);

        contexto.EnviarMensaje($"📊 Total clientes: {panel.TotalClientes}");

        contexto.EnviarMensaje("🕒 Interacciones recientes:");
        foreach (var interaccion in panel.InteraccionesRecientes)
        {
            contexto.EnviarMensaje($"- {interaccion}");
        }

        contexto.EnviarMensaje("📅 Reuniones próximas:");
        foreach (var reunion in panel.ReunionesProximas)
        {
            contexto.EnviarMensaje($"- {reunion.Asunto} en {reunion.Lugar} ({reunion.Fecha})");
        }

        return true;
    }
}