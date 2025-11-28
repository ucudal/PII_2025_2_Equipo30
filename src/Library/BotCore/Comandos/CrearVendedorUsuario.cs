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
    
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("Solo un usuario puede crear un vendedor.");
            return false;
        }
        
        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            //TODO 
        }
        contexto.EnviarMensaje("Hubo algún error.");
        return false;
    }
    
}