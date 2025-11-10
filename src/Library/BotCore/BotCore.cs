using Library.BotCore.Interfaces;

namespace Library.BotCore;

/// <summary>
/// Clase que contiene la lógica interna del bot.
/// </summary>
public class BotCore
{
    /// <summary>
    /// Diccionario que guarda los comandos asignados al bot.
    /// </summary>
    /// <remarks>
    /// La key es la id que tendrá el comando del bot, el usuario ejecuta el comando ingresando su numero correspondiente.
    /// </remarks>
    private Dictionary<int, IBotCommand> _comandos = new Dictionary<int, IBotCommand>(); //diccionario de comandos
    private int cantComandos = 1;
    
    /// <summary>
    /// Representa la información de la sesión actual.
    /// </summary>
    public Sesion Sesion { get; set; } = new Sesion();
    
    /// <summary>
    /// Asigna un comando al bot para poder ejecutarlo.
    /// </summary>
    /// <param name="comando"></param>
    /// <remarks>
    /// El numero asignado al comando es asignado según el orden en el que se ejecuta el metodo.
    /// </remarks>
    public void RegistrarComando(IBotCommand comando)
    {
        _comandos.Add(this.cantComandos, comando);
        cantComandos++;
    }

    /// <summary>
    /// Guarda un string con la lista de comandos asignados al bot y su número correspondiente.
    /// </summary>
    /// <returns>
    /// Un string de texto con el mensaje.
    /// </returns>
    public string MostrarComandos()
    {
        var texto = "🤖 Comandos disponibles:\n";
        foreach (var par in _comandos)
        {
            texto += $"{par.Key}. {par.Value.Nombre}\n";
        }
        return texto;
    }


    /// <summary>
    /// Metodo para pasar un texto a numero.
    /// </summary>
    /// <param name="textoNumerico"></param>
    /// <returns>
    /// <c>int</c> si el texto númerico se pudo pasar a integer.
    /// <c>null</c> si no se pudo transformar el texto númerico a integer.
    /// </returns>
    public int? TextoaNumero(string textoNumerico)
    {
        int numero = 0;
        try
        {
            numero = int.Parse(textoNumerico);
            return numero;
        }
        catch (Exception exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Procesa el número recibido por el tipo de usuario e intenta ejecutar el tipo de comando correspondiente.
    /// </summary>
    /// <param name="texto">opcion recibida por el usuario</param>
    /// <param name="contexto"></param>
    /// <returns>
    /// <c>true</c> si se pudo procesar la opción y se ejecutó el comando.
    /// <c>false</c> si no se pudo procesar la opción o no se ejecutó el comando.
    /// </returns>
    public bool ProcesarOpcion(string texto, IMessageContext contexto)
    {
        int? numeroOpcion = TextoaNumero(texto);

        if (!numeroOpcion.HasValue)
        {
            contexto.EnviarMensaje("⚠️ Opción no válida. Debes ingresar un número para ejecutar un comando.");
            return false;
        }

        if (_comandos.TryGetValue(numeroOpcion.Value, out IBotCommand comando))
        {
            comando.Ejecutar(contexto); // ejecuta el comando
            return true;
        }
        else
        {
            contexto.EnviarMensaje("❌ No existe un comando con ese número."); 
            return false;
        }
    }
}
