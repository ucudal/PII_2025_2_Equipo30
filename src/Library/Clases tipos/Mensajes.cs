namespace Library.Clases_tipos;

/// <summary>
/// Representa un conjunto de mensajes enviados y recibidos, con su asunto y descripción general.
/// </summary>
public class Mensajes
{
    /// <summary>
    /// Lista de mensajes enviados.
    /// </summary>
    public List<Mensaje> mensajesEnviados { get; set; } = new List<Mensaje>();
    /// <summary>
    /// Lista de mensajes recibidos.
    /// </summary>
    public List<Mensaje> mensajesRecibidos { get; set; } = new List<Mensaje>();
    /// <summary>
    /// Asunto general de los mensajes.
    /// </summary>
    public string Asunto { get; set; }
    /// <summary>
    /// Descripción general o cuerpo asociado a los mensajes.
    /// </summary>
    public string Descripcion { get; set; }

    /// <summary>
    /// Agrega un mensaje a la lista de mensajes recibidos.
    /// </summary>
    /// <param name="mensaje">Mensaje recibido.</param>
    /// <param name="fecha">Fecha asociada al mensaje recibido.</param>
    public void AgregarRecibidos(Mensaje mensaje, string fecha)
    {
        this.mensajesRecibidos.Add(mensaje);
    }

    /// <summary>
    /// Agrega un mensaje a la lista de mensajes enviados.
    /// </summary>
    /// <param name="mensaje">Mensaje enviado.</param>
    public void AgregarEnviados(Mensaje mensaje)
    {
        this.mensajesEnviados.Add(mensaje);
    }

    /// <summary>
    /// Define el asunto general de los mensajes.
    /// </summary>
    /// <param name="asunto">Texto del asunto.</param>
    public void AgregarAsunto(string asunto)
    {
        this.Asunto = asunto;
    }

    /// <summary>
    /// Define una descripción general para los mensajes.
    /// </summary>
    /// <param name="descripcion">Texto descriptivo del conjunto de mensajes.</param>
    public void AgregarDescripcion(string descripcion)
    {
        this.Descripcion = descripcion;
    }
}