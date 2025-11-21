using Library.BotCore.Interfaces;
using Library.Fachadas;

namespace Library.BotCore.Comandos;
/// <summary>
/// Permite crear un usuario siendo administrador.
/// </summary>
public class CrearUsuarioAdmin : IBotCommand
{
    public string Nombre { get; set; } = "Crear nuevo usuario";
    public string Descripcion { get; }
    private readonly BotCore _bot;
    private readonly FachadaRegistro _fachada;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bot">Instancia de BotCore.</param>
    /// <param name="fachada">Instancia de FachadaRegistro.</param>
    public CrearUsuarioAdmin(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }

    /// <summary>
    /// Ejecuta la creación de usuario como administrador solicitando nombre y clave.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Administrador")
        {
            contexto.EnviarMensaje("❌ Solo un administrador puede crear usuarios.");
            return false;
        }

        contexto.EnviarMensaje("Ingrese nombre del usuario: ");
        string nombre = contexto.EsperarRespuesta();
        contexto.EnviarMensaje("Ingrese clave: ");
        string clave = contexto.EsperarRespuesta();

        var usuario = _fachada.CrearUsuario(nombre, clave);
        if (usuario != null)
        {
            contexto.EnviarMensaje($"✅ Usuario {usuario.Nombre} creado correctamente.");
            return true;
        }

        contexto.EnviarMensaje("⚠️ Ya existe un usuario con ese nombre.");
        return false;
    }
}