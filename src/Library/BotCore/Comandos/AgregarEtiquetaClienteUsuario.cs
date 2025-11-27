using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;
/// <summary>
/// Agregar una etiqueta a un Cliente.
/// </summary>
public class AgregarEtiquetaClienteUsuario : IBotCommand
{
    public string Nombre { get; } = "Agregar etiqueta a un cliente.";
    public string Descripcion { get; }
    
    private BotCore _bot;
    private FachadaRegistro _fachada;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bot"></param>
    /// <param name="fachada"></param>
    public AgregarEtiquetaClienteUsuario(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    
    /// <summary>
    /// Lista las etiquetas disponibles, pide un id de cliente al usuario y una etiqueta a asignar para asignarla al cliente.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("Solo un usuario puede agregar una etiqueta a un cliente.");
            return false;
        }

        // Se usa is Usuario usuario para poder referenciar la instancia del usuario logueado en los comandos de fachada.
        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            try
            { 
                contexto.EnviarMensaje("Digite el id del cliente a asignar etiqueta.");
                int id = int.Parse(contexto.EsperarRespuesta());
                RegistroCliente cliente = usuario.BuscarClientePorId(id);
                if (cliente != null)
                {
                    contexto.EnviarMensaje($"¡Cliente encontrado!");
                    if (usuario.Etiquetas.Count > 0)
                    {
                        string texto = "Digite el número correspondiente a la etiqueta a asignar\n"; 
                        foreach (var etiqueta in usuario.Etiquetas) 
                        { 
                            texto += $"{etiqueta.Key} - {etiqueta.Value}\n";
                        }
                        contexto.EnviarMensaje(texto);
                        int opcion = int.Parse(contexto.EsperarRespuesta());
                        cliente.Cliente.Etiqueta = usuario.Etiquetas[opcion];
                        contexto.EnviarMensaje($"Se asignó la etiqueta {usuario.Etiquetas[opcion]} al cliente con id {id}");
                        return true;
                    }
                    else
                    {
                        contexto.EnviarMensaje($"No tienes etiquetas creadas, por ende no hay nada para asignar, crea una etiqueta con el comando Crear Etiqueta.");
                    }
                }
                else
                {
                    contexto.EnviarMensaje("Error, no se encontró al cliente, puede buscarlo con el comando Buscar Cliente o Listar Clientes.");
                    return false;
                }
            }
            catch (Exception)
            {
                contexto.EnviarMensaje("Hubo un error con la asingnación de etiquetas, intentelo de nuevo.");
            }
        }
        contexto.EnviarMensaje("Algo salió mal");
        return false;
    }
    
}