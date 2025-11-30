using System.Globalization;
using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

/// <summary>
/// Muestra todas las interacciones con un cliente, con o sin filtro, por tipo de interacción y por fecha.
/// </summary>
public class InteraccionesClienteUsuario : IBotCommand
{
    public string Nombre { get; } = "Ver interacciones con un cliente (con o sin filtro).";
    public string Descripcion { get; } = "Muestra todas las interacciones con un cliente.";
    private readonly BotCore _bot;
    private readonly FachadaRegistro _fachada;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="fachada"></param>
    /// <param name="bot"></param>
    public InteraccionesClienteUsuario(FachadaRegistro fachada, BotCore bot)
    {
        _fachada = fachada;
        _bot = bot;
    }
    /// <summary>
    /// Ejecuta el comando que Muestra las Interaciones.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("Solo un usuario ejecutar este comando.");
            return false;
        }

        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            contexto.EnviarMensaje("Ingrese el id del cliente para ver todas las interacciones.");
            try
            {
                int id = int.Parse(contexto.EsperarRespuesta());
                RegistroCliente cliente = usuario.BuscarClientePorId(id);
                if (cliente != null)
                {
                    contexto.EnviarMensaje($"¡Cliente encontrado!");
                    contexto.EnviarMensaje("A continuación, seleccione el filtro para ver las interacciones:\n" +
                                           "1 - Sin filtro (Ver todas las interacciones) (No recomendado)\n" +
                                           "2 - Ver interacciones mensajes\n" +
                                           "3 - Ver interacciones emails\n" +
                                           "4 - Ver interacciones llamadas\n" +
                                           "5 - Ver interacciones reuniones\n" +
                                           "6 - Ver interacciones ventas\n" +
                                           "7 - Ver interaccion cotización.");
                    try
                    {
                        int opcion = int.Parse(contexto.EsperarRespuesta());
                        switch (opcion)
                        {
                            case 1:
                                contexto.EnviarMensaje($"===Mensajes enviados===");
                                if (cliente.Mensajes.mensajesEnviados.Count > 0)
                                {
                                    foreach (var mensaje in cliente.Mensajes.mensajesEnviados)
                                    {
                                        contexto.EnviarMensaje($"Mensaje: {mensaje.Texto}\n" +
                                                               $"Fecha: {mensaje.Fecha}\n" +
                                                               $"Descripción: {mensaje.Descripcion}");
                                    }
                                }

                                contexto.EnviarMensaje($"===Mensajes recividos===\n");
                                if (cliente.Mensajes.mensajesRecibidos.Count > 0)
                                {
                                    foreach (var mensaje in cliente.Mensajes.mensajesRecibidos)
                                    {
                                        contexto.EnviarMensaje($"Mensaje: {mensaje.Texto}\n" +
                                                               $"Fecha: {mensaje.Fecha}\n" +
                                                               $"Descripción: {mensaje.Descripcion}");
                                    }
                                }

                                contexto.EnviarMensaje($"===Emails enviados===\n");
                                if (cliente.Emails.Enviados.Count > 0)
                                {
                                    foreach (var email in cliente.Emails.Enviados)
                                    {
                                        contexto.EnviarMensaje($"Email: {email.Texto}\n" +
                                                               $"Fecha: {email.Fecha}\n" +
                                                               $"Descripción: {email.Descripcion}");
                                    }
                                }

                                contexto.EnviarMensaje($"===Emails recibidos===\n");
                                if (cliente.Emails.Recibidos.Count > 0)
                                {
                                    foreach (var email in cliente.Emails.Recibidos)
                                    {
                                        contexto.EnviarMensaje($"Email: {email.Texto}\n" +
                                                               $"Fecha: {email.Fecha}\n" +
                                                               $"Descripción: {email.Descripcion}");
                                    }
                                }

                                contexto.EnviarMensaje($"===Llamadas enviadas===");
                                if (cliente.Llamadas.Enviados.Count > 0)
                                {
                                    foreach (var llamada in cliente.Llamadas.Enviados)
                                    {
                                        contexto.EnviarMensaje($"Llamada: {llamada.Asunto}\n" +
                                                               $"Fecha: {llamada.Fecha}\n" +
                                                               $"Descripción: {llamada.Descripcion}");
                                    }
                                }

                                contexto.EnviarMensaje($"===Llamadas recibidas===");
                                if (cliente.Llamadas.Recibidos.Count > 0)
                                {
                                    foreach (var llamada in cliente.Llamadas.Recibidos)
                                    {
                                        contexto.EnviarMensaje($"Llamada: {llamada.Asunto}\n" +
                                                               $"Fecha: {llamada.Fecha}\n" +
                                                               $"Descripción: {llamada.Descripcion}");
                                    }
                                }

                                contexto.EnviarMensaje($"===Reunion===");
                                if (cliente.Reunion != null)
                                {
                                    contexto.EnviarMensaje($"Lugar: {cliente.Reunion.Lugar}\n" +
                                                           $"Fecha: {cliente.Reunion.Fecha}\n" +
                                                           $"Asunto: {cliente.Reunion.Asunto}\n" +
                                                           $"Descripción: {cliente.Reunion.Descripcion}");
                                }

                                contexto.EnviarMensaje($"===Ventas===");
                                if (cliente.Ventas.ListaVentas.Count > 0)
                                {
                                    foreach (var venta in cliente.Ventas.ListaVentas)
                                    {
                                        contexto.EnviarMensaje($"Venta: {venta.Descripcion}\n" +
                                                               $"Precio: {venta.Precio}\n" +
                                                               $"Fecha: {venta.Fecha}");
                                    }
                                }

                                break;

                            case 2:
                                contexto.EnviarMensaje("Ingrese a partir de que fecha mostrar los mensajes (DD/MM/AA)");
                                string fecha = contexto.EsperarRespuesta();
                                if (DateTime.TryParseExact(fecha, "dd/MM/yy", null, DateTimeStyles.None, out DateTime fechaFiltro))
                                {
                                    if (cliente.Mensajes.mensajesEnviados.Count > 0)
                                    {
                                        foreach (var mensaje in cliente.Mensajes.mensajesEnviados)
                                        {
                                            if (DateTime.TryParseExact(mensaje.Fecha, "dd/MM/yy", null, DateTimeStyles.None,
                                                    out DateTime fechaMensaje) && (fechaMensaje > fechaFiltro))
                                            {
                                                contexto.EnviarMensaje($"Mensaje: {mensaje.Texto}\n" +
                                                                       $"Fecha: {mensaje.Fecha}\n" +
                                                                       $"Descripción: {mensaje.Descripcion}");
                                            }
                                        }
                                    }

                                    if (cliente.Mensajes.mensajesRecibidos.Count > 0)
                                    {
                                        foreach (var mensaje in cliente.Mensajes.mensajesRecibidos)
                                        {
                                            if (DateTime.TryParseExact(mensaje.Fecha, "dd/MM/yy", null, DateTimeStyles.None,
                                                    out DateTime fechaMensaje) && (fechaMensaje > fechaFiltro))
                                            {
                                                contexto.EnviarMensaje($"Mensaje: {mensaje.Texto}\n" +
                                                                       $"Fecha: {mensaje.Fecha}\n" +
                                                                       $"Descripción: {mensaje.Descripcion}");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    contexto.EnviarMensaje("Error: La fecha ingresada no es válida. Debe tener el formato DD/MM/AA.");
                                    return false;
                                }

                                break;

                            case 3:
                                contexto.EnviarMensaje("Ingrese a partir de que fecha mostrar los emails (DD/MM/AA)");
                                string fechaEmail = contexto.EsperarRespuesta();

                                if (DateTime.TryParseExact(fechaEmail, "dd/MM/yy", null, DateTimeStyles.None, out DateTime fechaFiltroEmail))
                                {
                                    contexto.EnviarMensaje("===Emails enviados===");
                                    if (cliente.Emails.Enviados.Count > 0)
                                    {
                                        foreach (var email in cliente.Emails.Enviados)
                                        {
                                            if (DateTime.TryParseExact(email.Fecha, "dd/MM/yy", null, DateTimeStyles.None,
                                                    out DateTime fechaEmailItem) && fechaEmailItem >= fechaFiltroEmail)
                                            {
                                                contexto.EnviarMensaje($"Email: {email.Texto}\n" +
                                                                       $"Fecha: {email.Fecha}\n" +
                                                                       $"Descripción: {email.Descripcion}");
                                            }
                                        }
                                    }

                                    contexto.EnviarMensaje("===Emails recibidos===");
                                    if (cliente.Emails.Recibidos.Count > 0)
                                    {
                                        foreach (var email in cliente.Emails.Recibidos)
                                        {
                                            if (DateTime.TryParseExact(email.Fecha, "dd/MM/yy", null, DateTimeStyles.None,
                                                    out DateTime fechaEmailItem) && fechaEmailItem >= fechaFiltroEmail)
                                            {
                                                contexto.EnviarMensaje($"Email: {email.Texto}\n" +
                                                                       $"Fecha: {email.Fecha}\n" +
                                                                       $"Descripción: {email.Descripcion}");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    contexto.EnviarMensaje("Error: La fecha ingresada no es válida. Debe tener el formato DD/MM/AA.");
                                    return false;
                                }
                                break;

                            case 4:
                                contexto.EnviarMensaje("Ingrese a partir de que fecha mostrar las llamadas (DD/MM/AA)");
                                string fechaLlamada = contexto.EsperarRespuesta();

                                if (DateTime.TryParseExact(fechaLlamada, "dd/MM/yy", null, DateTimeStyles.None, out DateTime fechaFiltroLlamada))
                                {
                                    contexto.EnviarMensaje("===Llamadas enviadas===");
                                    if (cliente.Llamadas.Enviados.Count > 0)
                                    {
                                        foreach (var llamada in cliente.Llamadas.Enviados)
                                        {
                                            if (DateTime.TryParseExact(llamada.Fecha, "dd/MM/yy", null, DateTimeStyles.None,
                                                    out DateTime fechaLlamadaItem) && fechaLlamadaItem >= fechaFiltroLlamada)
                                            {
                                                contexto.EnviarMensaje($"Llamada: {llamada.Asunto}\n" +
                                                                       $"Fecha: {llamada.Fecha}\n" +
                                                                       $"Descripción: {llamada.Descripcion}");
                                            }
                                        }
                                    }

                                    contexto.EnviarMensaje("===Llamadas recibidas===");
                                    if (cliente.Llamadas.Recibidos.Count > 0)
                                    {
                                        foreach (var llamada in cliente.Llamadas.Recibidos)
                                        {
                                            if (DateTime.TryParseExact(llamada.Fecha, "dd/MM/yy", null, DateTimeStyles.None,
                                                    out DateTime fechaLlamadaItem) && fechaLlamadaItem >= fechaFiltroLlamada)
                                            {
                                                contexto.EnviarMensaje($"Llamada: {llamada.Asunto}\n" +
                                                                       $"Fecha: {llamada.Fecha}\n" +
                                                                       $"Descripción: {llamada.Descripcion}");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    contexto.EnviarMensaje("Error: La fecha ingresada no es válida. Debe tener el formato DD/MM/AA.");
                                    return false;
                                }
                                break;

                            case 5:
                                contexto.EnviarMensaje("Ingrese a partir de que fecha mostrar las reuniones (DD/MM/AA)");
                                string fechaReunion = contexto.EsperarRespuesta();

                                if (DateTime.TryParseExact(fechaReunion, "dd/MM/yy", null, DateTimeStyles.None, out DateTime fechaFiltroReunion))
                                {
                                    if (cliente.Reunion != null)
                                    {
                                        if (DateTime.TryParseExact(cliente.Reunion.Fecha, "dd/MM/yy", null, DateTimeStyles.None,
                                                out DateTime fechaReunionItem) && fechaReunionItem >= fechaFiltroReunion)
                                        {
                                            contexto.EnviarMensaje($"Lugar: {cliente.Reunion.Lugar}\n" +
                                                                   $"Fecha: {cliente.Reunion.Fecha}\n" +
                                                                   $"Asunto: {cliente.Reunion.Asunto}\n" +
                                                                   $"Descripción: {cliente.Reunion.Descripcion}");
                                        }
                                    }
                                }
                                else
                                {
                                    contexto.EnviarMensaje("Error: La fecha ingresada no es válida. Debe tener el formato DD/MM/AA.");
                                    return false;
                                }
                                break;

                            case 6:
                                contexto.EnviarMensaje("Ingrese a partir de que fecha mostrar las ventas (DD/MM/AA)");
                                string fechaVenta = contexto.EsperarRespuesta();

                                if (DateTime.TryParseExact(fechaVenta, "dd/MM/yy", null, DateTimeStyles.None, out DateTime fechaFiltroVenta))
                                {
                                    contexto.EnviarMensaje("===Ventas===");
                                    if (cliente.Ventas.ListaVentas.Count > 0)
                                    {
                                        foreach (var venta in cliente.Ventas.ListaVentas)
                                        {
                                            if (DateTime.TryParseExact(venta.Fecha, "dd/MM/yy", null, DateTimeStyles.None,
                                                    out DateTime fechaVentaItem) && fechaVentaItem >= fechaFiltroVenta)
                                            {
                                                contexto.EnviarMensaje($"Venta: {venta.Descripcion}\n" +
                                                                       $"Precio: {venta.Precio}\n" +
                                                                       $"Fecha: {venta.Fecha}");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    contexto.EnviarMensaje("Error: La fecha ingresada no es válida. Debe tener el formato DD/MM/AA.");
                                    return false;
                                }
                                break;

                            case 7:
                                contexto.EnviarMensaje("Ingrese a partir de que fecha mostrar las cotizaciones (DD/MM/AA)");
                                string fechaCot = contexto.EsperarRespuesta();

                                if (DateTime.TryParseExact(fechaCot, "dd/MM/yy", null, DateTimeStyles.None, out DateTime fechaFiltroCot))
                                {
                                    if (cliente.Precio != null)
                                    {
                                        if (DateTime.TryParseExact(cliente.Precio.Fecha, "dd/MM/yy", null, DateTimeStyles.None,
                                                out DateTime fechaCotItem) && fechaCotItem >= fechaFiltroCot)
                                        {
                                            contexto.EnviarMensaje($"Monto: {cliente.Precio.Costo}\n" +
                                                                   $"Fecha: {cliente.Precio.Fecha}\n" +
                                                                   $"Descripción: {cliente.Precio.Descripcion}");
                                        }
                                    }
                                }
                                else
                                {
                                    contexto.EnviarMensaje("Error: La fecha ingresada no es válida. Debe tener el formato DD/MM/AA.");
                                    return false;
                                }
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        contexto.EnviarMensaje("Hubo un error al interpretar su opción.");
                        return false;
                    }
                }
                else
                {
                    contexto.EnviarMensaje("No se encontró al cliente, puede buscar su id utilizando el comando Buscar Cliente o Listar Clientes.");
                    return false;
                }
            }
            catch (Exception)
            {
                contexto.EnviarMensaje("Algo salió mal.");
                return false;
            }
        }
        contexto.EnviarMensaje("Hubo un error.");
        return false;
    }
}