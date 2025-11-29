namespace Library.Clases_principales;
/// <summary>
/// Representa a un vendedor dentro del sistema, encargado de gestionar sus clientes.
/// </summary>
public class Vendedor
{
    
    public string Nombre { get; set; }
    public string Clave { get; set; }
    
    public int Id { get; set; }
    
    /// <summary>
    /// Lista de <see cref="Cliente"/> asociados a este vendedor.
    /// </summary>
    public List<Cliente> Clientes { get; set; }
    
    /// <summary>
    /// Metodo para asistir con el logeo.
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="clave"></param>
    /// <returns>True si credenciales coinciden, False si no.</returns>
    public bool ValidarCredenciales(string nombre, string clave)
    {
        return this.Nombre == nombre && this.Clave == clave;
    }
    
    ///<summary>
    /// Agrega un nuevo cliente a la lista de clientes del vendedor.
    /// </summary>
    public void AgregarCliente(Cliente cliente)
    {
        if (cliente != null)
            Clientes.Add(cliente);
    }
    
    /// <summary>
    /// Asigna un cliente de este vendedor a otro vendedor.
    /// </summary>
    /// <param name="cliente">Instancia del <see cref="Cliente"/> que se desea reasignar.</param>
    /// <param name="otroVendedor">Instancia del <see cref="Vendedor"/> al cual se le asignará el cliente.</param>
    /// <returns>
    /// <c>true</c> si el cliente fue reasignado correctamente;  
    /// <c>false</c> si el cliente o el vendedor destino son nulos,  
    /// o si el cliente no pertenece al vendedor actual.
    /// </returns>
    /// <remarks>
    /// El método realiza los siguientes pasos:
    /// <list type="number">
    /// <item><description>Verifica que el cliente y el otro vendedor existan.</description></item>
    /// <item><description>Comprueba que el cliente esté asignado a este vendedor.</description></item>
    /// <item><description>Elimina al cliente de la lista actual.</description></item>
    /// <item><description>Agrega al cliente a la lista de clientes del otro vendedor.</description></item>
    /// </list>
    /// </remarks>
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

    /// <summary>
    /// Busca un <see cref="Cliente"/> por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns><see cref="Cliente"/> si se encontró, null si no."/></returns>
    public Cliente BuscarClientePorId(int id)
    {
        foreach (var cliente in Clientes)
        {
            if (cliente.Id == id)
            {
                return cliente;
            }
        }
        return null;
    }

    private static int ContadorId = 1;
    
    /// <summary>
    /// Constructor de <see cref="Vendedor"/>
    /// </summary>
    /// <param name="nombre">Nombre del vendedor.</param>
    /// <param name="clave">Clave o contraseña del vendedor.</param>
    public Vendedor(string nombre, string clave)
    {
        this.Clientes = new List<Cliente>();
        this.Nombre = nombre;
        this.Clave = clave;
        this.Id = ContadorId++;
    }
}