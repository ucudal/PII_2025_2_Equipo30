namespace Library.BotCore.Interfaces
{
    /// <summary>
    /// Representa el contexto de una interacción con el bot, independientemente de la API.
    /// </summary>
    public interface IMessageContext
    {
        /// <summary>
        /// ID de usuario según la API (Debe integrarse con la API).
        /// </summary>
        string UsuarioId { get; } 
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        string UsuarioNombre { get; }

        /// <summary>
        /// Envía la respuesta al usuario (Debe integrarse con la API).
        /// </summary>
        /// <param name="texto">Mensaje a enviar</param>
        void EnviarMensaje(string texto); // Para enviar la respuesta al usuario
        string EsperarRespuesta(); // Para recibir entrada (en consola) o por API.
    }
}