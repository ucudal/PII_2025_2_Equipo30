using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Clases_tipos;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

/// <summary>
/// Registra una cotización a un cliente.
/// </summary>
public class RegistrarCotizacionClienteUsuario : IBotCommand
{
    public string Nombre { get; } = "Registrar cotización a un cliente.";
    public string Descripcion { get; } = "Registra una cotización realizada a un cliente.";
    private readonly BotCore _bot;
    private readonly FachadaRegistro _fachada;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fachada">Instancia FachadaRegistro.</param>
    /// <param name="bot">Instancia BotCore.</param>
    public RegistrarCotizacionClienteUsuario(FachadaRegistro fachada, BotCore bot)
    {
        _fachada = fachada;
        _bot = bot;
    }

    /// <summary>
    /// Asigna una cotización a un cliente a partir de una solicitud de datos al usuario (descripción, precio, fecha).
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("Solo un usuario puede registrar la cotización a un cliente.");
            return false;
        }

        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            contexto.EnviarMensaje("Ingrese el id del cliente al cual va a asignar la cotización.");
            try
            {
                int id = int.Parse(contexto.EsperarRespuesta());
                RegistroCliente cliente = usuario.BuscarClientePorId(id);
                if (cliente != null)
                {
                    contexto.EnviarMensaje($"¡Cliente encontrado!\n" +
                                           $"Digite el nombre del articulo que cotizó.");
                    Precio precio = new Precio();
                    precio.Descripcion = contexto.EsperarRespuesta();
                    contexto.EnviarMensaje($"Digite el precio de cotización.");
                    precio.Costo = int.Parse(contexto.EsperarRespuesta());
                    contexto.EnviarMensaje("Digite la fecha de la cotización (DD/MM/AA).");
                    precio.Fecha = contexto.EsperarRespuesta();

                    cliente.Precio = precio;
                    contexto.EnviarMensaje("¡Cotización asignada correctamente!");
                    return true;
                }
                else
                {
                    contexto.EnviarMensaje("No se encontró al cliente, puede buscar su id utilizando el comando Buscar Cliente o Listar Clientes.");
                    return false;
                }
            }
            catch (Exception)
            {
                contexto.EnviarMensaje("Algo salió mal al asignar la cotización.");
                return false;
            }
        }
        contexto.EnviarMensaje("Algo salió mal.");
        return false;
    }
}