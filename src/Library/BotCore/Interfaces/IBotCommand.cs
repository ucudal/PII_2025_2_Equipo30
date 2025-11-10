namespace Library.BotCore.Interfaces
{
    /// <summary>
    /// Interfaz para comandos de bot.
    /// </summary>
    public interface IBotCommand
    {
        /// <summary>
        /// Nombre del metodo.
        /// </summary>
        string Nombre { get; }
        /// <summary>
        /// Descripción del metodo.
        /// </summary>
        string Descripcion { get; }
        /// <summary>
        /// Metodo para ejecutar el comando.
        /// </summary>
        /// <param name="context">mensaje recibido de la API.</param>
        /// <returns></returns>
        bool Ejecutar(IMessageContext context);
    }
}