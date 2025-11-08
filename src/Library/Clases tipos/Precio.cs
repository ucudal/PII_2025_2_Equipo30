namespace Library.Clases_tipos;

/// <summary>
/// Representa una cotización o precio propuesto, con su descripción, fecha y costo asociado.
/// </summary>
public class Precio
{
    /// <summary>
    /// Descripción o detalle de la cotización.
    /// </summary>
    public string Descripcion { get; set; }
    /// <summary>
    /// Fecha en la que se emitió la cotización.
    /// </summary>
    public string Fecha { get; set; }
    /// <summary>
    /// Costo o monto cotizado.
    /// </summary>
    public int Costo { get; set; }
}