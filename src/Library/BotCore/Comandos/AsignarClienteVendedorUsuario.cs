using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

/// <summary>
/// Asigna un cliente a un <see cref="Vendedor"/> siendo usuario.
/// </summary>
public class AsignarClienteVendedorUsuario : IBotCommand
{
     public string Nombre { get; } = "Asignar cliente a otro vendedor siendo Usuario.";
    public string Descripcion { get; } = "Asigna el cliente a otro vendedor para distribuir el trabajo.";
    private BotCore _bot;
    private FachadaRegistro _fachada;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="bot"></param>
    /// <param name="fachada"></param>
    public AsignarClienteVendedorUsuario(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }

    /// <summary>
    /// Ejecuta el comando de asignación de registro del cliente a otro vendedor.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("Solo un Usuario puede asignar un cliente a otro vendedor con este comando.");
            return false;
        }

        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            contexto.EnviarMensaje("Ingrese el id del cliente a asignar.");
            int id;
            if (int.TryParse(contexto.EsperarRespuesta(), out id))
            {
                RegistroCliente cliente = usuario.BuscarClientePorId(id);
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
                            if (usuario.AsignarClienteAVendedor(cliente, otroVendedor))
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