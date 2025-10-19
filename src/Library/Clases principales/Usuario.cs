namespace Library.Clases_principales;

public class Usuario
{
    public string Nombre { get; set; }
    public string Clave { get; set; }

    public List<RegistroCliente> Clientes = new List<RegistroCliente>();
    
    public bool ModificarCliente(int id, string nuevoNombre = null, string nuevoApellido = null,
        string nuevoTelefono = null, string nuevoEmail = null,
        string nuevoGenero = null, string nuevoCumple = null,
        string nuevaEtiqueta = null)
    {
        foreach (var cliente in Clientes)
        {
            if (cliente.Cliente.Id == id)
            {
                if (nuevoNombre != null) cliente.Cliente.Nombre = nuevoNombre;
                if (nuevoApellido != null) cliente.Cliente.Apellido = nuevoApellido;
                if (nuevoTelefono != null) cliente.Cliente.Telefono = nuevoTelefono;
                if (nuevoEmail != null) cliente.Cliente.Email = nuevoEmail;
                if (nuevoGenero != null) cliente.Cliente.Genero = nuevoGenero;
                if (nuevoCumple != null) cliente.Cliente.fechaNacimiento = nuevoCumple;
                if (nuevaEtiqueta != null) cliente.Cliente.Etiqueta = nuevaEtiqueta;
                return true; //true si se encontro al cliente y se pudo modificar
            }
        }
        return false; //false si no se encontró al cliente
    }

    public bool EliminarCliente(Cliente cliente) 
    {
        for (int i = Clientes.Count - 1; i >= 0; i--) //recorre la lista de atras en adelante para no afectar los indices y elimina el registro del cliente.
        {
            if (Clientes[i].Cliente.Id == cliente.Id)
            {
                Clientes.RemoveAt(i);
                return true; //devuelve true si pudo eliminar al usuario
            }
        }
        return false; //devuelve false si no pudo eliminar al usuario
    }
    
    public RegistroCliente BuscarClientePorId(int id)
    {
        foreach (var registro in Clientes)
        {
            if (registro.Cliente.Id == id)
            {
                return registro; // Encontramos el cliente y devolvemos el registro
            }
        }
        return null; // No se encontró ningún cliente con ese Id
    }

    
    public List<Cliente> ListarClientes()
    {
        List<Cliente> listaClientes = new List<Cliente>();
        foreach (var registro in Clientes)
        {
            listaClientes.Add(registro.Cliente); // Agregamos el cliente a la lista
        }
        return listaClientes;
    }
    
}