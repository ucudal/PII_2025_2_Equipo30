using Library.BotCore.Interfaces;
using Library.Clases_principales;

namespace Library.BotCore.Comandos;
/// <summary>
/// Muestra una lista de los Clientes de un Vendedor.
/// </summary>
public class ListarClientesVendedor : IBotCommand
{
    public string Nombre { get; } = "Listar clientes del vendedor.";
    public string Descripcion { get; } = "Lista todos los clientes del vendedor con sus datos e id";
    private BotCore _bot;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bot"></param>
    public ListarClientesVendedor(BotCore bot)
    {
        _bot = bot;
    }
    
    /// <summary>
    /// Ejecuta el comando que Lista los clientes.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Vendedor")
        {
            contexto.EnviarMensaje("Solo un vendedor puede listar los clientes.");
            return false;
        }

        if (_bot.Sesion.UsuarioActual is Vendedor vendedor)
        {
            contexto.EnviarMensaje("Listando clientes...");
            foreach (var c in vendedor.Clientes)
            {
                contexto.EnviarMensaje($"Cliente: {c.Nombre} {c.Apellido}\n" +
                                       $"Id: {c.Id}");
            }
            contexto.EnviarMensaje("Ya se listaron todos los clientes.");
            return true;
        }
        contexto.EnviarMensaje($"Ocurrió un error.");
        return false;
    }
}