namespace Library.Clases_tipos;

/// <summary>
/// Representa un mensaje individual con su contenido y fecha.
/// </summary>
public class Mensaje
{
    /// <summary>
    /// Fecha del mensaje.
    /// </summary>
    public string Fecha { get; set; }
    /// <summary>
    /// Contenido o texto del mensaje.
    /// </summary>
    public string Texto { get; set; }
    
    public string Descripcion { get; set; }

    public void AgregarDescripcion(string descripcion)
    {
        this.Descripcion = descripcion;
    }

}