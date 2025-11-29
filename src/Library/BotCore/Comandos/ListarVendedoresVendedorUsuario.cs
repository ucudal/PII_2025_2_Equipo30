using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;
/// <summary>
/// Lista todos los clientes del vendedor con sus datos.
/// </summary>
public class ListarVendedoresVendedorUsuario : IBotCommand
{
    public string Nombre { get; } = "Listar clientes del vendedor.";
    public string Descripcion { get; } = "Lista todos los clientes del vendedor con sus datos e id";
    private BotCore _bot;
    private FachadaRegistro _fachada;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bot"></param>
    /// <param name="fachada"></param>
    public ListarVendedoresVendedorUsuario(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    
    /// <summary>
    /// Ejecuta el comando que Lista los Vendedores.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Vendedor" || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("Solo un vendedor o usuario puede listar los clientes.");
            return false;
        }

        if (_bot.Sesion.UsuarioActual is Vendedor vendedor || _bot.Sesion.UsuarioActual is Usuario usuario)
        {
            contexto.EnviarMensaje("Listando vendedores...");
            foreach (var v in _fachada.Vendedores)
            {
                contexto.EnviarMensaje($"Vendedor: {v.Nombre}\n" +
                                       $"Id: {v.Id}");
            }
            contexto.EnviarMensaje("Ya se listaron todos los vendedores.");
            return true;
        }
        contexto.EnviarMensaje("Error inesperado.");
        return false;
    }
}