namespace Library.Clases_tipos;
/// <summary>
/// Representa una llamada con fecha y asunto relacionado.
/// </summary>
public class Llamada
{
    /// <summary>
    /// Fecha en la que se realizó o recibió la llamada.
    /// </summary>
    public string Fecha { get; set; }
    /// <summary>
    /// Asunto o tema principal de la llamada.
    /// </summary>
    public string Asunto { get; set; }
    
    public string Descripcion { get; set; }

    public void AgregarDescripcion(string descripcion)
    {
        this.Descripcion = descripcion;
    }
}