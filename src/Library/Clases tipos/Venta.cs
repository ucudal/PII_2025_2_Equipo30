namespace Library.Clases_tipos;

/// <summary>
/// Representa una venta o transacción comercial con su descripción, fecha y precio.
/// </summary>
public class Venta
{
    /// <summary>
    /// Descripción del producto o servicio vendido.
    /// </summary>
    public string Descripcion { get; set; }
    /// <summary>
    /// Fecha en la que se realizó la venta.
    /// </summary>
    public string Fecha { get; set; }
    /// <summary>
    /// Precio o valor total de la venta.
    /// </summary>
    public int Precio { get; set; }
}