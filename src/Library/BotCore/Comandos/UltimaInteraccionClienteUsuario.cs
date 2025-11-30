using System.Data;
using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;
/// <summary>
/// Muestra una lista de clientes con los que no se interactua desde cierta fecha.
/// </summary>
public class UltimaInteraccionClienteUsuario : IBotCommand
{
    public string Nombre { get; } = "Listar clientes desde ultima interacción.";
    public string Descripcion { get; } = "Lista clientes con los que no se interactua desde cierta fecha.";
    private BotCore _bot;
    private FachadaRegistro _fachada;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="bot"></param>
    /// <param name="fachada"></param>
    public UltimaInteraccionClienteUsuario(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    /// <summary>
    /// Ejecuta el comando de una lista de clientes con los que no se interactua desde cierta fecha.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("Solo un usuario puede ver la ultima interacion a un cliente.");
            return false;
        }

        // Se usa is Usuario usuario para poder referenciar la instancia del usuario logueado en los comandos de fachada.
        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            DateTime fechaFiltro;
            contexto.EnviarMensaje("Digite una fecha a partir de la cual listar las ultimas interacciónes:");
            if (DateTime.TryParseExact(contexto.EsperarRespuesta(), "dd/MM/yy", null, DateTimeStyles.None,
                    out fechaFiltro))
            {
                contexto.EnviarMensaje("Clientes que verifican las condiciones:");
                int cantidad = 0;
                foreach (var cliente in usuario.Clientes)
                {
                    //Si la ultima interacción es más antigua que la fecha filtro imprime los datos.
                    if (cliente.UltimaInteraccion() < fechaFiltro)
                    {
                        cantidad++;
                        contexto.EnviarMensaje($"Cliente: {cliente.Cliente.Nombre} {cliente.Cliente.Apellido}\n" +
                                               $"Id: {cliente.Cliente.Id}\n" +
                                               $"Ultimo contacto: {cliente.UltimaInteraccion()}");
                    }
                }

                if (cantidad == 0)
                {
                    contexto.EnviarMensaje("No hay clientes que cumplan las condiciones.");
                    return true;
                }
                else
                {
                    contexto.EnviarMensaje("Ya se imprimieron todos los clientes.");
                    return true;
                }
            }
            else
            {
                contexto.EnviarMensaje("El formato de fecha es invalido, recuerde que debe ser DD/MM/AA.");
                return false;
            }
        }
        contexto.EnviarMensaje("Algo salió mal.");
        return false;
    }
}