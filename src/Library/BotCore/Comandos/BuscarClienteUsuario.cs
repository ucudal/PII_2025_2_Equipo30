using System.Runtime.CompilerServices;
using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

/// <summary>
/// Busca un cliente por nombre, apellido, telefono o email siendo usuario.
/// </summary>
public class BuscarClienteUsuario : IBotCommand
{
    public string Nombre { get; } = "Buscar Cliente por Nombre,Apellido, Telefono o Email";
    public string Descripcion { get; }
    private BotCore _bot;
    private FachadaRegistro _fachada;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="bot">Instancia BotCore.</param>
    /// <param name="fachada">Instancia FachadaRegistro.</param>
    public BuscarClienteUsuario(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    
    /// <summary>
    /// Ejecuta la busqueda del cliente en base al Nombre, Apellido, Telefono o Email.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("Solo un usuario puede buscar a un cliente.");
            return false;
        }
        
        // Se usa is Usuario usuario para poder referenciar la instancia del usuario logueado en los comandos de fachada.
        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            
            try
            {
                contexto.EnviarMensaje($"Antes de comenzar, ingrese el tipo de dato a utilizar para buscar al cliente:\n" +
                                       $"1 - Nombre\n" +
                                       $"2 - Apellido\n" +
                                       $"3 - Telefono\n" +
                                       $"4 - Email");
                int opcion = int.Parse(contexto.EsperarRespuesta());
                List<Cliente> resultados = new List<Cliente>();
                string dato;
                switch (opcion)
                {
                    case 1:
                        contexto.EnviarMensaje("Ingrese el nombre del cliente:");
                        dato = contexto.EsperarRespuesta();
                        resultados = _fachada.BuscarClientes(usuario, nombre: dato);
                        break;
                    case 2:
                        contexto.EnviarMensaje("Ingrese el apellido del cliente:");
                        dato = contexto.EsperarRespuesta();
                        resultados =  _fachada.BuscarClientes(usuario, apellido: dato);
                        break;
                    case 3:
                        contexto.EnviarMensaje("Ingrese el telefono del cliente:");
                        dato = contexto.EsperarRespuesta();
                        resultados = _fachada.BuscarClientes(usuario, telefono: dato);
                        break;
                    case 4:
                        contexto.EnviarMensaje("Ingrese el email del cliente:");
                        dato = contexto.EsperarRespuesta();
                        resultados = _fachada.BuscarClientes(usuario, email: dato);
                        break;
                }

                if (resultados.Count > 0)
                {
                    foreach (var cliente in resultados)
                    {
                        contexto.EnviarMensaje($"Posile coincidencia:\n" +
                                               $"Se encontró al siguiente cliente:\n" +
                                               $"Nombre:{cliente.Nombre}\n" +
                                               $"Apellido: {cliente.Apellido}\n" +
                                               $"Id: {cliente.Id}\n" +
                                               $"Telefono: {cliente.Telefono}" +
                                               $"Email: {cliente.Email}");
                    }
                    contexto.EnviarMensaje("Ya se imprimieron todas las coincidencias.");
                }
            }
            catch (Exception)
            {
                contexto.EnviarMensaje("Hubo un error al buscar al cliente.");
                return false;
            }
        }
        contexto.EnviarMensaje("No se encontró al cliente.");
        return false;
    }
}