using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

/// <summary>
/// Modifica o registra los datos de un cliente a partir de su id.
/// </summary>
public class ModificarClienteUsuario : IBotCommand
{
    public string Nombre { get; } = "Modificar datos de un cliente";
    public string Descripcion { get; } = "Modificar datos de un cliente a partir de su id";
    private readonly BotCore _bot;
    private readonly FachadaRegistro _fachada;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fachada">Instancia FachadaRegistro.</param>
    /// <param name="bot">Instancia BotCore.</param>
    public ModificarClienteUsuario(FachadaRegistro fachada, BotCore bot)
    {
        _fachada = fachada;
        _bot = bot;
    }

    /// <summary>
    /// Ejecuta el intento de modificar o regsitrar los datos de un cliente a partir de su id.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("Solo un usuario puede modificar los datos de un cliente.");
            return false;
        }

        contexto.EnviarMensaje("Ingrese el id del cliente a modificar: ");
        try
        {
            int idcliente = int.Parse(contexto.EsperarRespuesta());

            // Se usa is Usuario usuario para poder referenciar la instancia del usuario logueado en los comandos de fachada.
            if (_bot.Sesion.UsuarioActual is Usuario usuario)
            {
                if (usuario.BuscarClientePorId(idcliente) != null)
                {
                    contexto.EnviarMensaje(
                        "¡Cliente encontrado! A continuación, ingrese el numero correspondiente al dato que desea modificar o registrar: \n" +
                        "1 - Nombre.\n" +
                        "2 - Apellido. \n" +
                        "3 - Telefono. \n" +
                        "4 - Email. \n" +
                        "5 - Genero. \n" +
                        "6 - Cumpleaños.\n" +
                        "7 - Etiqueta."
                    );

                    int opcion = int.Parse(contexto.EsperarRespuesta());
                    string nuevoValor;

                    switch (opcion)
                    {
                        case 1:
                            contexto.EnviarMensaje("Ingrese el nuevo nombre:");
                            nuevoValor = contexto.EsperarRespuesta();
                            if (_fachada.ModificarCliente(usuario, idcliente, nuevoNombre: nuevoValor))
                                contexto.EnviarMensaje("✔ Nombre actualizado exitosamente.");
                            else
                                contexto.EnviarMensaje("❌ No se pudo modificar el cliente.");
                            break;

                        case 2:
                            contexto.EnviarMensaje("Ingrese el nuevo apellido:");
                            nuevoValor = contexto.EsperarRespuesta();
                            if (_fachada.ModificarCliente(usuario, idcliente, nuevoApellido: nuevoValor))
                                contexto.EnviarMensaje("✔ Apellido actualizado exitosamente.");
                            else
                                contexto.EnviarMensaje("❌ No se pudo modificar el cliente.");
                            break;

                        case 3:
                            contexto.EnviarMensaje("Ingrese el nuevo teléfono:");
                            nuevoValor = contexto.EsperarRespuesta();
                            if (_fachada.ModificarCliente(usuario, idcliente, nuevoTelefono: nuevoValor))
                                contexto.EnviarMensaje("✔ Teléfono actualizado exitosamente.");
                            else
                                contexto.EnviarMensaje("❌ No se pudo modificar el cliente.");
                            break;

                        case 4:
                            contexto.EnviarMensaje("Ingrese el nuevo email:");
                            nuevoValor = contexto.EsperarRespuesta();
                            if (_fachada.ModificarCliente(usuario, idcliente, nuevoEmail: nuevoValor))
                                contexto.EnviarMensaje("✔ Email actualizado exitosamente.");
                            else
                                contexto.EnviarMensaje("❌ No se pudo modificar el cliente.");
                            break;

                        case 5:
                            contexto.EnviarMensaje("Ingrese el nuevo género:");
                            nuevoValor = contexto.EsperarRespuesta();
                            if (_fachada.ModificarCliente(usuario, idcliente, nuevoGenero: nuevoValor))
                                contexto.EnviarMensaje("✔ Género actualizado exitosamente.");
                            else
                                contexto.EnviarMensaje("❌ No se pudo modificar el cliente.");
                            break;

                        case 6:
                            contexto.EnviarMensaje("Ingrese la nueva fecha de cumpleaños:");
                            nuevoValor = contexto.EsperarRespuesta();
                            if (_fachada.ModificarCliente(usuario, idcliente, nuevoCumple: nuevoValor))
                                contexto.EnviarMensaje("✔ Cumpleaños actualizado exitosamente.");
                            else
                                contexto.EnviarMensaje("❌ No se pudo modificar el cliente.");
                            break;

                        case 7:
                            contexto.EnviarMensaje("Ingrese la nueva etiqueta:");
                            nuevoValor = contexto.EsperarRespuesta();
                            if (_fachada.ModificarCliente(usuario, idcliente, nuevaEtiqueta: nuevoValor))
                                contexto.EnviarMensaje("✔ Etiqueta actualizada exitosamente.");
                            else
                                contexto.EnviarMensaje("❌ No se pudo modificar el cliente.");
                            break;

                        default:
                            contexto.EnviarMensaje("❌ Opción inválida.");
                            break;
                    }

                    return true;
                }
            }
        }
        catch
        {
            contexto.EnviarMensaje("Hubo un error al modificar los datos del cliente.");
        }

        contexto.EnviarMensaje("No se encontró al cliente.");
        return false;
    }
}
