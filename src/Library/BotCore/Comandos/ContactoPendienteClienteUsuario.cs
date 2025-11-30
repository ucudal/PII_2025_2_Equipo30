using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;
/// <summary>
/// Muestra cuales clientes no se les contacta hace cierto tiempo.
/// </summary>
public class ContactoPendienteClienteUsuario : IBotCommand
{
    public string Nombre { get; } = "Listar clientes a los cuales no se respondió.";
    public string Descripcion { get; } = "Lista clientes a los cuales no se les respondió el ultimo mensaje.";
    private BotCore _bot;
    private FachadaRegistro _fachada;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="bot"></param>
    /// <param name="fachada"></param>
    public ContactoPendienteClienteUsuario(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    /// <summary>
    /// Ejecuta el comando que lista los clientes con mensajes pendientes de respuesta.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("Solo un usuario puede verificar los contactos pendientes a clientes.");
            return false;
        }

        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            DateTime ultimoEnviado;
            DateTime ultimoRecibido;
            contexto.EnviarMensaje("Listando clientes que esperan respuesta...");
            int contador = 0;
            foreach (var cliente in usuario.Clientes)
            {
                if ((DateTime.TryParseExact(cliente.Mensajes.mensajesEnviados.Last().Fecha, "dd/MM/yy", null, DateTimeStyles.None, out ultimoEnviado)) && (DateTime.TryParseExact(cliente.Mensajes.mensajesRecibidos.Last().Fecha, "dd/MM/yy", null, DateTimeStyles.None, out ultimoRecibido)))
                {
                    if (ultimoEnviado < ultimoRecibido)
                    {
                        contexto.EnviarMensaje($"Cliente {cliente.Cliente.Nombre} {cliente.Cliente.Apellido}\n" +
                                               $"Id: {cliente.Cliente.Id}\n" +
                                               $"Ultimo mensaje recibido el {cliente.Mensajes.mensajesRecibidos.Last().Fecha}");
                    }
                }
            }

            if (contador == 0)
            {
                contexto.EnviarMensaje("No hay clientes que aguarden respuesta");
                return true;
            }
            else
            {
                contexto.EnviarMensaje("Ya se imprimieron todos los clientes que aguardan respuesta.");
                return true;
            }
        }
        contexto.EnviarMensaje("Algo salió mal.");
        return false;
    }
}