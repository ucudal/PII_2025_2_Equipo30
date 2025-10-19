namespace Library.Clases_principales;

public class Vendedor
{
    public string Nombre { get; set; }
    public string Clave { get; set; }
    public List<Cliente> Clientes { get; set; }

    public bool AsignarClienteAotroVendedor(Cliente cliente, Vendedor otroVendedor)
    {
        if (cliente == null || otroVendedor == null) //Verifica que exista el cliente y vendedor
            return false;
        
        bool clienteEnEsteVendedor = false; //Verifica que el cliente esté asignado a este vendedor
        foreach (var c in Clientes)
        {
            if (c.Id == cliente.Id)
            {
                clienteEnEsteVendedor = true;
                break;
            }
        }
        if (!clienteEnEsteVendedor)
            return false;
        
        for (int i = 0; i < Clientes.Count; i++) //Eliminar cliente de este vendedor
        {
            if (Clientes[i].Id == cliente.Id)
            {
                Clientes.RemoveAt(i);
                break;
            }
        }
        
        otroVendedor.Clientes.Add(cliente); //Asignar cliente a otro vendedor

        return true;
    }
    
    public Vendedor(string nombre, string clave)
    {
        this.Clientes = new List<Cliente>();
        this.Nombre = nombre;
        this.Clave = clave;
    }
}