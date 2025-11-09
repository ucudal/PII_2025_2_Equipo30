namespace Library.Clases_tipos;

/// <summary>
/// Representa una reunión o encuentro con un cliente o contacto.
/// </summary>
public class Reunion
{
    /// <summary>
    /// Lugar donde se realizará o realizó la reunión.
    /// </summary>
    public string Lugar { get; set; }
    /// <summary>
    /// Fecha de la reunión.
    /// </summary>
    public string Fecha { get; set; }
    /// <summary>
    /// Asunto o tema tratado en la reunión.
    /// </summary>
    public string Asunto { get; set; }
    
    public string Descripcion { get; set; }

    public void AgregarDescripcion(string descripcion)
    {
        this.Descripcion = descripcion;
    }

}