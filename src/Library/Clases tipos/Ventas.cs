namespace Library.Clases_tipos;

public class Ventas
{
    public List<Venta> ListaVentas { get; set; }

    public void AgregarVenta(Venta venta)
    {
        this.ListaVentas.Add(venta);
    }
    
}