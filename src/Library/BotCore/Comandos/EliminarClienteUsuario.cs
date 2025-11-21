using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

/// <summary>
/// Comando que elimina un cliente a partir de su id siendo usuario.
/// </summary>
public class EliminarClienteUsuario : IBotCommand
{
    public string Nombre { get; } = "Eliminar Cliente";
    public string Descripcion { get; }

    private BotCore _bot;
    private FachadaRegistro _fachada;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bot">Instancia BotCore.</param>
    /// <param name="fachada">Instancia FachadaRegistro.</param>
    public EliminarClienteUsuario(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    
    /// <summary>
    /// Ejecuta la eliminación del cliente a partir de su id.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("Solo un usuario puede eliminar a un cliente.");
            return false;
        }

        // Se usa is Usuario usuario para poder referenciar la instancia del usuario logueado en los comandos de fachada.
        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            contexto.EnviarMensaje("Ingrese el id del cliente a eliminar: ");
            string id = contexto.EsperarRespuesta();
            try
            {
                RegistroCliente cliente = usuario.BuscarClientePorId(int.Parse(id));
                _fachada.EliminarCliente(usuario, cliente.Cliente);
                contexto.EnviarMensaje("Cliente eliminado con exito, esta acción es irreversible.");
                return true;
            }
            catch (Exception)
            {
                contexto.EnviarMensaje($"Hubo un error al intentar eliminar al cliente\n" +
                                       $"¿formato id incorrecto?");
                return false;
            }
        }
        contexto.EnviarMensaje("Hubo un error al intentar eliminar al cliente");
        return false;
    }
}