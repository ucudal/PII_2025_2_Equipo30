namespace Library.Clases_tipos;
/// <summary>
/// Representa un correo electronico individual con su texto y fecha de envío o recepción.
/// </summary>
public class Email
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