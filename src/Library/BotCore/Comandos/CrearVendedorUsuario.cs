using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

/// <summary>
/// Crea un nuevo vendedor
/// </summary>
public class CrearVendedorUsuario : IBotCommand
{
    public string Nombre { get; } = "Crea un nuevo vendedor";
    public string Descripcion { get; }
    private BotCore _bot;
    private FachadaRegistro _fachada;

    public CrearVendedorUsuario(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    
    /// <summary>
    /// Ejecuta el comando que Crea un Vendedor con un nombre y Clave.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("Solo un usuario puede crear un vendedor.");
            return false;
        }
        
        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            contexto.EnviarMensaje("Ingrese el nombre del vendedor:");
            string nombre = contexto.EsperarRespuesta();
            contexto.EnviarMensaje("Ingrese la clave del vendedor:");
            string clave = contexto.EsperarRespuesta();
            if (_fachada.CrearVendedor(nombre, clave))
            {
                contexto.EnviarMensaje("Vendedor creado correctamente.");
                return true;
            }
            else
            {
                contexto.EnviarMensaje("ERROR: Ya existe un vendedor con ese nombre.");
                return false;
            }
        }
        contexto.EnviarMensaje("Hubo algún error.");
        return false;
    }
    
}