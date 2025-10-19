namespace Library.Clases;

public class Vendedor
{
    public string Nombre { get; set; }
    public string Clave { get; set; }
    public List<Cliente> Clientes { get; set; }

    public void AsignarCliente(Cliente cliente, Vendedor vendedor)
    {
        //TODO
    }
    
    public Vendedor(string nombre, string clave)
    {
        this.Clientes = new List<Cliente>();
        this.Nombre = nombre;
        this.Clave = clave;
    }
}