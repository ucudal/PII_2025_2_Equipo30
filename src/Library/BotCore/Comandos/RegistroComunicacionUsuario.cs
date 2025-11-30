using System;
using Library.BotCore.Interfaces;
using Library.BotCore;
using Library.Clases_principales;
using Library.Clases_tipos;
using Library.Fachadas;

namespace Library.BotCore.Comandos
{
    /// <summary>
    /// Registra comunicaciones (llamadas, reuniones, mensajes, emails) de un Usuario con sus clientes.
    /// Incluye la posibilidad de agregar notas o comentarios a cada interacción.
    /// </summary>
    public class RegistroComunicacionUsuario : IBotCommand
    {
        public string Nombre { get; } = "Registrar comunicación con cliente";
        public string Descripcion { get; } = "Registra llamadas, reuniones, mensajes o emails (enviados/recibidos) y permite agregar notas";

        private readonly BotCore _bot;
        private readonly FachadaRegistro _fachada;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fachada">Instancia FachadaRegistro.</param>
        /// <param name="bot">Instancia BotCore.</param>
        public RegistroComunicacionUsuario(BotCore bot, FachadaRegistro fachada)
        {
            _bot = bot;
            _fachada = fachada;
        }
        /// <summary>
        /// Ejecuta el comando de comunicacion y utilisa los tipos de comunicacion como Llamada etc.
        /// </summary>
        /// <param name="contexto"></param>
        /// <returns></returns>
        public bool Ejecutar(IMessageContext contexto)
        {
            // Validación de sesión y rol
            if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
            {
                contexto.EnviarMensaje("❌ Solo un usuario puede registrar comunicaciones.");
                return false;
            }

            if (!(_bot.Sesion.UsuarioActual is Usuario usuario))
            {
                contexto.EnviarMensaje("⚠️ No se encontró el usuario en sesión.");
                return false;
            }

            try
            {
                // Selección del tipo de comunicación
                contexto.EnviarMensaje(
                    "Seleccione el tipo de comunicación:\n" +
                    "1 - Llamada\n" +
                    "2 - Reunión\n" +
                    "3 - Mensaje\n" +
                    "4 - Email");
                string tipoTexto = contexto.EsperarRespuesta();

                int tipo;
                bool parseTipo = int.TryParse(tipoTexto, out tipo);
                if (!parseTipo || tipo < 1 || tipo > 4)
                {
                    contexto.EnviarMensaje("⚠️ Opción inválida.");
                    return false;
                }

                // Enviada/recibida (no aplica a reunión)
                bool Recibido = false;
                if (tipo != 2)
                {
                    contexto.EnviarMensaje("¿La comunicación fue enviada o recibida?\n1 - Enviada\n2 - Recibida");
                    string envio = contexto.EsperarRespuesta();

                    if (envio == "1")
                    {
                        Recibido = false;
                    }
                    else if (envio == "2")
                    {
                        Recibido = true;
                    }
                    else
                    {
                        contexto.EnviarMensaje("⚠️ Opción inválida.");
                        return false;
                    }
                }

                // Cliente destino
                contexto.EnviarMensaje("Ingrese el id del cliente:");
                string idTexto = contexto.EsperarRespuesta();

                int clienteId;
                bool parseId = int.TryParse(idTexto, out clienteId);
                if (!parseId)
                {
                    contexto.EnviarMensaje("⚠️ Id de cliente inválido.");
                    return false;
                }

                var registro = usuario.BuscarClientePorId(clienteId);
                if (registro == null)
                {
                    contexto.EnviarMensaje("❌ No se encontró el cliente.");
                    return false;
                }

                // Campos comunes
                contexto.EnviarMensaje("Ingrese la fecha (DD/MM/AA):");
                string fecha = contexto.EsperarRespuesta();

                // Ejecución según tipo
                if (tipo == 1) // Llamada
                {
                    contexto.EnviarMensaje("Ingrese el asunto/tema de la llamada:");
                    string asunto = contexto.EsperarRespuesta();

                    contexto.EnviarMensaje("Descripción breve de la llamada:");
                    string descripcion = contexto.EsperarRespuesta();

                    contexto.EnviarMensaje("Notas o comentarios adicionales:");
                    string notas = contexto.EsperarRespuesta();

                    var llamada = new Llamada
                    {
                        Fecha = fecha,
                        Asunto = asunto,
                        Descripcion = descripcion
                    };

                    // Guardar notas en la misma propiedad Descripcion si se proporcionaron
                    if (!string.IsNullOrWhiteSpace(notas))
                    {
                        // Si ya hay descripción, concatenamos la nota para no perder información
                        if (string.IsNullOrWhiteSpace(llamada.Descripcion))
                        {
                            llamada.Descripcion = notas;
                        }
                        else
                        {
                            llamada.Descripcion = $"{llamada.Descripcion} | Nota: {notas}";
                        }
                    }

                    bool ok;
                    if (Recibido)
                    {
                        ok = usuario.RegistrarLlamadaRecibida(clienteId, llamada);
                    }
                    else
                    {
                        ok = usuario.ActualizarCliente(clienteId, llamada);
                    }

                    if (ok)
                    {
                        contexto.EnviarMensaje("✅ Llamada registrada correctamente.");
                        return true;
                    }
                    else
                    {
                        contexto.EnviarMensaje("❌ No se pudo registrar la llamada.");
                        return false;
                    }
                }
                else if (tipo == 2) // Reunión
                {
                    contexto.EnviarMensaje("Ingrese el lugar de la reunión:");
                    string lugar = contexto.EsperarRespuesta();

                    contexto.EnviarMensaje("Ingrese el asunto/tema de la reunión:");
                    string asunto = contexto.EsperarRespuesta();

                    contexto.EnviarMensaje("Descripción breve de la reunión:");
                    string descripcion = contexto.EsperarRespuesta();

                    contexto.EnviarMensaje("Notas o comentarios adicionales:");
                    string notas = contexto.EsperarRespuesta();

                    var reunion = new Reunion
                    {
                        Fecha = fecha,
                        Lugar = lugar,
                        Asunto = asunto,
                        Descripcion = descripcion
                    };

                    if (!string.IsNullOrWhiteSpace(notas))
                    {
                        if (string.IsNullOrWhiteSpace(reunion.Descripcion))
                        {
                            reunion.Descripcion = notas;
                        }
                        else
                        {
                            reunion.Descripcion = $"{reunion.Descripcion} | Nota: {notas}";
                        }
                    }

                    bool ok = usuario.ActualizarCliente(clienteId, reunion);

                    if (ok)
                    {
                        contexto.EnviarMensaje("✅ Reunión registrada correctamente.");
                        return true;
                    }
                    else
                    {
                        contexto.EnviarMensaje("❌ No se pudo registrar la reunión.");
                        return false;
                    }
                }
                else if (tipo == 3) // Mensaje
                {
                    contexto.EnviarMensaje("Ingrese el asunto/tema del mensaje:");
                    string asunto = contexto.EsperarRespuesta();

                    contexto.EnviarMensaje("Ingrese el texto del mensaje:");
                    string texto = contexto.EsperarRespuesta();

                    contexto.EnviarMensaje("Notas o comentarios adicionales:");
                    string notas = contexto.EsperarRespuesta();

                    string textoFinal;
                    if (string.IsNullOrWhiteSpace(asunto))
                    {
                        textoFinal = texto;
                    }
                    else
                    {
                        textoFinal = $"{asunto}: {texto}";
                    }

                    var mensaje = new Mensaje
                    {
                        Fecha = fecha,
                        Texto = textoFinal,
                        Descripcion = null
                    };

                    if (!string.IsNullOrWhiteSpace(notas))
                    {
                        mensaje.Descripcion = notas;
                    }

                    bool ok;
                    if (Recibido)
                    {
                        ok = usuario.RegistrarMensajeRecibido(clienteId, mensaje);
                    }
                    else
                    {
                        ok = usuario.ActualizarCliente(clienteId, mensaje);
                    }

                    if (ok)
                    {
                        contexto.EnviarMensaje("✅ Mensaje registrado correctamente.");
                        return true;
                    }
                    else
                    {
                        contexto.EnviarMensaje("❌ No se pudo registrar el mensaje.");
                        return false;
                    }
                }
                else if (tipo == 4) // Email
                {
                    contexto.EnviarMensaje("Ingrese el asunto del email:");
                    string asunto = contexto.EsperarRespuesta();

                    contexto.EnviarMensaje("Ingrese el cuerpo del email:");
                    string cuerpo = contexto.EsperarRespuesta();

                    contexto.EnviarMensaje("Notas o comentarios adicionales:");
                    string notas = contexto.EsperarRespuesta();

                    string textoFinal;
                    if (string.IsNullOrWhiteSpace(asunto))
                    {
                        textoFinal = cuerpo;
                    }
                    else
                    {
                        textoFinal = $"{asunto}: {cuerpo}";
                    }

                    var email = new Email
                    {
                        Fecha = fecha,
                        Texto = textoFinal,
                        Descripcion = null
                    };

                    if (!string.IsNullOrWhiteSpace(notas))
                    {
                        email.Descripcion = notas;
                    }

                    if (Recibido)
                    {
                        registro.Emails.AgregarRecibidos(email);
                    }
                    else
                    {
                        registro.Emails.AgregarEnviados(email);
                    }

                    contexto.EnviarMensaje("✅ Email registrado correctamente.");
                    return true;
                }
                else
                {
                    contexto.EnviarMensaje("⚠️ Tipo no soportado.");
                    return false;
                }
            }
            catch (Exception)
            {
                contexto.EnviarMensaje("❌ Ocurrió un error al registrar la comunicación.");
                return false;
            }
        }
    }
}
