using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Clases_tipos;
using Library.Fachadas;

namespace Library.BotCore.Comandos;
/// <summary>
/// Registra la venta realizada a un cliente.
/// </summary>
public class RegistrarVentaClienteVendedor : IBotCommand
{
    public string Nombre { get; } = "Registrar venta a cliente siendo vendedor.";
    public string Descripcion { get; } = "Registra la venta a un cliente, incluye descripción, precio y fecha.";

    private BotCore _bot;
    private FachadaRegistro _fachada;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="bot"></param>
    /// <param name="fachada"></param>
    public RegistrarVentaClienteVendedor(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }

    /// <summary>
    /// Solicita los datos (descripción, precio y fecha) de la venta y los agrega al registro del cliente.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Vendedor")
        {
            contexto.EnviarMensaje("Solo un vendedor puede registrar la venta a un cliente con este comando.");
            return false;
        }
        
        // Se usa is Vendedor vendedor para poder referenciar la instancia del vendedor logueado en los comandos de fachada.
        if (_bot.Sesion.UsuarioActual is Vendedor vendedor)
        {
            contexto.EnviarMensaje("Ingrese el id del cliente al cual va a asignar la venta.");
            try
            {
                int id = int.Parse(contexto.EsperarRespuesta());
                RegistroCliente cliente = vendedor.BuscarClientePorId(id);
                if (cliente != null)
                {
                    contexto.EnviarMensaje($"¡Cliente encontrado!\n" +
                                           $"Digite el nombre del articulo que le vendió:");
                    Venta venta = new Venta();
                    venta.Descripcion = contexto.EsperarRespuesta();
                    contexto.EnviarMensaje("Digite el precio por el que se realizó la venta.");
                    venta.Precio = int.Parse(contexto.EsperarRespuesta());
                    contexto.EnviarMensaje("Digite la fecha en la que realizó la venta DD/MM/AA:");
                    venta.Fecha = contexto.EsperarRespuesta();
                    cliente.Ventas.AgregarVenta(venta);
                    contexto.EnviarMensaje("¡Se registró la venta correctamente!");
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
                contexto.EnviarMensaje("Algo salió mal al asignar la venta.");
                return false;
            }
        }
        contexto.EnviarMensaje("Hubo un error al asignar la venta.");
        return false;
    }
}