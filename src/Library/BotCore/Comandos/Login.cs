using Library.BotCore.Interfaces;
using Library.BotCore;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

/// <summary>
/// Comando de logueo, permite a un vendedor, usuario o admin acceder ciertos tipos de comando en especifico.
/// </summary>
public class LoginCommand : IBotCommand
{
    public string Nombre { get; set; } = "Iniciar sesión";
    public string Descripcion { get; } = "Inicia sesión como administrador, usuario o vendedor.";
    private readonly BotCore _bot;
    private readonly FachadaRegistro _fachada;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="bot">Instancia de BotCore.</param>
    /// <param name="fachada">Fachada de FachadaRegistro.</param>
    public LoginCommand(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }

    /// <summary>
    /// Ejecuta el intento de logeo.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns>
    /// <c>true</c> si se pudo realizar el login.
    /// <c>false</c> si no se pudo realizar el login.
    /// </returns>
    /// <remarks>
    /// Verifica si las credenciales existen en la fachada.
    /// </remarks>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (_bot.Sesion.EstaLogeado)
        {
            contexto.EnviarMensaje("⚠️ Ya hay una sesión activa. Cierra sesión antes de iniciar otra.");
            return false;
        }

        contexto.EnviarMensaje("Ingrese su nombre:");
        string nombre = contexto.EsperarRespuesta();

        contexto.EnviarMensaje("Ingrese su clave:");
        string clave = contexto.EsperarRespuesta();

        // --- Intentar login por tipo ---
        var admin = _fachada.LoginAdministrador(nombre, clave);
        if (admin != null)
        {
            _bot.Sesion.IniciarSesion(admin, "Administrador");
            contexto.EnviarMensaje("✅ Sesión iniciada como Administrador.");
            return true;
        }

        var usuario = _fachada.LoginUsuario(nombre, clave);
        if (usuario != null)
        {
            _bot.Sesion.IniciarSesion(usuario, "Usuario");
            contexto.EnviarMensaje($"✅ Sesión iniciada como Usuario: {usuario.Nombre}.");
            return true;
        }

        var vendedor = _fachada.LoginVendedor(nombre, clave);
        if (vendedor != null)
        {
            _bot.Sesion.IniciarSesion(vendedor, "Vendedor");
            contexto.EnviarMensaje($"✅ Sesión iniciada como Vendedor: {vendedor.Nombre}.");
            return true;
        }
        
        contexto.EnviarMensaje("❌ Credenciales incorrectas. Intente nuevamente.");
        return false;
    }
}