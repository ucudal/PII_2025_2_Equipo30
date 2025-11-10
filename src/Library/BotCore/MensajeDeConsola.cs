namespace Library.BotCore
{
    /// <summary>
    /// Interfaz para permitir la comunicación con mensajes de consola.
    /// </summary>
    public class MensajeDeConsola : Interfaces.IMessageContext
    {
        public string UsuarioId => "consola";
        public string UsuarioNombre => "Terminal";

        public void EnviarMensaje(string texto)
        {
            Console.WriteLine(texto);
        }

        public string EsperarRespuesta()
        {
            return Console.ReadLine();
        }
    }
}