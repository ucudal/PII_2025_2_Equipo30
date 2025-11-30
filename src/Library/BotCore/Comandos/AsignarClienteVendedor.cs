using System.Runtime.InteropServices.JavaScript;
using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;
/// <summary>
/// Asigna un Cliente a un Vededor.
/// </summary>
public class AsignarClienteVendedor : IBotCommand
{
    public string Nombre { get; } = "Asignar cliente a otro vendedor.";
    public string Descripcion { get; } = "Asigna el cliente a otro vendedor para distribuir el trabajo en equipo.";
    private BotCore _bot;
    private FachadaRegistro _fachada;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="bot"></param>
    /// <param name="fachada"></param>
    public AsignarClienteVendedor(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    /// <summary>
    /// Ejecuta el comando de asignación de cliente a otro vendedor.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Vendedor")
        {
            contexto.EnviarMensaje("Solo un vendedor puede asignar un cliente a otro vendedor.");
            return false;
        }

        if (_bot.Sesion.UsuarioActual is Vendedor vendedor)
        {
            contexto.EnviarMensaje("Ingrese el id del cliente a asignar.");
            int id;
            if (int.TryParse(contexto.EsperarRespuesta(), out id))
            {
                Cliente cliente = vendedor.BuscarClientePorId(id);
                if (cliente != null)
                {
                    contexto.EnviarMensaje(
                        "Cliente encontrado, ingrese el id del vendedor al cual asignara el cliente");
                    int idVendedor;
                    Vendedor otroVendedor = null;
                    if (int.TryParse(contexto.EsperarRespuesta(), out idVendedor))
                    {
                        foreach (var v in _fachada.Vendedores)
                        {
                            if (v.Id == idVendedor)
                            {
                                otroVendedor = v;
                            }
                        }

                        if (otroVendedor != null)
                        {
                            contexto.EnviarMensaje("Vendedor encontrado, asignando al cliente...");
                            if (vendedor.AsignarClienteAotroVendedor(cliente, otroVendedor))
                            {
                                contexto.EnviarMensaje("Asignación existosa.");
                                return true;
                            }
                            else
                            {
                                contexto.EnviarMensaje("Hubo un error.");
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    contexto.EnviarMensaje("Cliente no encontrado.");
                    return false;
                }
            }
            else
            {
                contexto.EnviarMensaje("Error: El formato de id es invalido.");
                return false;
            }
        }
        contexto.EnviarMensaje("Surgió algún problema.");
        return false;
    }
}