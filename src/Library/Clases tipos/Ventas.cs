namespace Library.Clases_tipos;

/// <summary>
/// Representa una colección de ventas asociadas a un cliente o vendedor.
/// </summary>
public class Ventas
{
    /// <summary>
    /// Lista de ventas registradas.
    /// </summary>
    public List<Venta> ListaVentas { get; set; } = new List<Venta>();

    /// <summary>
    /// Agrega una nueva venta al registro de ventas.
    /// </summary>
    public void AgregarVenta(Venta venta)
    {
        this.ListaVentas.Add(venta);
    }
    
}