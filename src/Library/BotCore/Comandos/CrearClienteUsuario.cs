using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

public class CrearClienteUsuario : IBotCommand
{
    public string Nombre { get; set; } = "Crear nuevo cliente";
    public string Descripcion { get; }
    private readonly BotCore _bot;
    private readonly FachadaRegistro _fachada;

    public CrearClienteUsuario(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }

    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("❌ Solo un usuario puede crear clientes.");
            return false;
        }
        contexto.EnviarMensaje("Ingrese el nombre del cliente:");
        string nombrecliente = contexto.EsperarRespuesta();
        contexto.EnviarMensaje("Ingrese el apellido del cliente:");
        string apellidocliente = contexto.EsperarRespuesta();
        contexto.EnviarMensaje("Ingrese el télefono del cliente:");
        string telefonocliente = contexto.EsperarRespuesta();
        contexto.EnviarMensaje("Ingrese el email del cliente:");
        string emailcliente = contexto.EsperarRespuesta();

        try
        {
            if (_bot.Sesion.UsuarioActual is Usuario usuario)
            {
                if (_fachada.CrearCliente(usuario, nombrecliente, apellidocliente, telefonocliente,
                        emailcliente))
                {
                    contexto.EnviarMensaje("El cliente se creó correctamente! Si se equivocó al introducir algún dato puede modificar los datos del cliente.");
                    return true;
                }
            }
        }
        catch
        {
            contexto.EnviarMensaje("Hubo algún error al crear al cliente.");
        }
        return false;
    }
}