using System;
using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos
{
    /// <summary>
    /// Comando que permite a un Usuario crear nuevas etiquetas para organizar y segmentar clientes.
    /// </summary>
    public class CrearEtiquetaUsuario : IBotCommand
    {
        public string Nombre { get; } = "Crear etiqueta de cliente";
        public string Descripcion { get; } = "Permite definir etiquetas para organizar y segmentar clientes.";

        private readonly BotCore _bot;
        private readonly FachadaRegistro _fachada;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="fachada"></param>
        public CrearEtiquetaUsuario(BotCore bot, FachadaRegistro fachada)
        {
            _bot = bot;
            _fachada = fachada;
        }
        /// <summary>
        /// Ejecuta el comando que Crea una Etiqueta con un nombre y Id.
        /// </summary>
        /// <param name="contexto"></param>
        /// <returns></returns>
        public bool Ejecutar(IMessageContext contexto)
        {
            // Validar sesión y rol
            if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
            {
                contexto.EnviarMensaje("❌ Solo un usuario puede crear etiquetas.");
                return false;
            }

            if (!(_bot.Sesion.UsuarioActual is Usuario usuario))
            {
                contexto.EnviarMensaje("⚠️ No se encontró el usuario en sesión.");
                return false;
            }

            try
            {
                // Pedir nombre de la etiqueta
                contexto.EnviarMensaje("Ingrese el nombre de la nueva etiqueta:");
                string nombreEtiqueta = contexto.EsperarRespuesta();

                if (string.IsNullOrWhiteSpace(nombreEtiqueta))
                {
                    contexto.EnviarMensaje("⚠️ El nombre de la etiqueta no puede estar vacío.");
                    return false;
                }

                // Generar un ID único para la etiqueta
                int nuevoId;
                if (usuario.Etiquetas.Count == 0)
                {
                    nuevoId = 1;
                }
                else
                {
                    nuevoId = usuario.Etiquetas.Keys.Max() + 1;
                }

                // Guardar en el diccionario
                usuario.Etiquetas[nuevoId] = nombreEtiqueta;

                contexto.EnviarMensaje($"✅ Etiqueta '{nombreEtiqueta}' creada con ID {nuevoId}.");
                return true;
            }
            catch (Exception)
            {
                contexto.EnviarMensaje("❌ Ocurrió un error al crear la etiqueta.");
                return false;
            }
        }
    }
}
