using Library.Clases_tipos;

namespace Library.Clases_principales;

/// <summary>
/// Administra y almacena una lista de <see cref="RegistroCliente"/>.
/// </summary>
public class Usuario
{
    public string Nombre { get; set; }
    public string Clave { get; set; }

    /// <summary>
    /// Lista de <see cref="RegistroCliente"/> que contiene al <see cref="Cliente"/> y sus interacciones.
    /// </summary>
    public List<RegistroCliente> Clientes = new List<RegistroCliente>();
    
    /// <summary>
    /// Modifica uno o más datos de un cliente en <see cref="Clientes"/>
    /// </summary>
    /// <param name="id">Id del cliente que se quiere modificar.</param>
    /// <param name="nuevoNombre">Nuevo nombre del <see cref="Cliente"/>.</param>
    /// <param name="nuevoApellido">Nuevo apellido del <see cref="Cliente"/></param>
    /// <param name="nuevoTelefono">Nuevo telefono del  <see cref="Cliente"/></param>
    /// <param name="nuevoEmail">Nuevo Email del <see cref="Cliente"/></param>
    /// <param name="nuevoGenero">Nuevo Genero del <see cref="Cliente"/></param>
    /// <param name="nuevoCumple">Nueva fecha de nacimiento del <see cref="Cliente"/></param>
    /// <param name="nuevaEtiqueta">Nueva etiqueta del <see cref="Cliente"/></param>
    /// <returns>
    /// <c>true</c> Si se encontró el campo a modificar y se modificó.
    /// <c>false</c> Si no se encontró al cliente.
    /// </returns>
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
                if (nuevoCumple != null) cliente.Cliente.FechaNacimiento = nuevoCumple;
                if (nuevaEtiqueta != null) cliente.Cliente.Etiqueta = nuevaEtiqueta;
                return true; //true si se encontro al cliente y se pudo modificar
            }
        }
        return false; //false si no se encontró al cliente
    }
    
    public bool AgregarEtiqueta(int clienteId, string etiqueta)
    {
        return ModificarCliente(clienteId, nuevaEtiqueta: etiqueta);
    }

    /// <summary>
    /// Elimina a un cliente de la lista <see cref="Clientes"/>
    /// </summary>
    /// <param name="cliente">Cliente a eliminar.</param>
    /// <returns>
    /// <c>true</c> Si se encontro y eliminó al cliente.
    /// <c>false</c> Si no se encontó o no se eliminó al cliente.
    /// </returns>
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
        return false; //devuelve false si no pudo eliminar al cliente
    }
    
    /// <summary>
    /// Busca a un cliente por id.
    /// </summary>
    /// <param name="id">Id del cliente a buscar.</param>
    /// <returns>
    /// <c>RegistroCliente</c> si se encontró al cliente.
    /// <c>Null</c> si no se encontró al cliente.
    /// </returns>
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

    
    /// <summary>
    /// Obtiene una lista de todos los clientes registrados.
    /// </summary>
    /// <returns>
    /// Una lista de <see cref="Cliente"/> que representan todos los clientes actualmente registrados.
    /// </returns>
    /// <remarks>
    /// Este método recorre la colección interna <c>Clientes</c> y extrae 
    /// cada objeto <see cref="Cliente"/> asociado, devolviendo una nueva lista.
    /// </remarks> 
    public List<Cliente> ListarClientes()
    {
        List<Cliente> listaClientes = new List<Cliente>();
        foreach (var registro in Clientes)
        {
            listaClientes.Add(registro.Cliente); // Agregamos el cliente a la lista
        }
        return listaClientes;
    }
    
    /// <summary>
    /// Actualiza la información del <c>registroCliente</c> de un cliente identificado por su ID,
    /// agregando o modificando datos según el tipo del objeto proporcionado.
    /// </summary>
    /// <param name="clienteId">Identificador único del cliente a actualizar.</param>
    /// <param name="dato">
    /// Objeto que contiene la información a actualizar. 
    /// Puede ser una instancia de <see cref="Mensaje"/>, <see cref="Llamada"/>,
    /// <see cref="Precio"/>, <see cref="Reunion"/> o <see cref="Venta"/>.
    /// </param>
    /// <returns>
    /// <c>true</c> si el cliente fue encontrado y la actualización se realizó correctamente;  
    /// <c>false</c> si no se encontró el cliente o el tipo de dato no es válido.
    /// </returns>
    /// <remarks>
    /// Este método busca el cliente por su identificador mediante <c>BuscarClientePorId</c>.
    /// Dependiendo del tipo del objeto <paramref name="dato"/>, actualiza diferentes colecciones 
    /// o propiedades dentro del registro del cliente:
    /// <list type="bullet">
    /// <item><description><see cref="Mensaje"/> → Se agrega a la lista de mensajes enviados.</description></item>
    /// <item><description><see cref="Llamada"/> → Se agrega a la lista de llamadas realizadas.</description></item>
    /// <item><description><see cref="Precio"/> → Se registra el nuevo precio asociado.</description></item>
    /// <item><description><see cref="Reunion"/> → Se asigna la reunión actual del cliente.</description></item>
    /// <item><description><see cref="Venta"/> → Se agrega a la lista de ventas realizadas.</description></item>
    /// </list>
    /// </remarks>
    public bool ActualizarCliente(int clienteId, object dato)
    {
        var registro = BuscarClientePorId(clienteId);
        if (registro == null) return false;

        switch (dato)
        {
            case Mensaje mensaje:
            {
                registro.Mensajes.AgregarEnviados(mensaje);
                return true;
            }

            case Llamada llamada:
            {
                registro.Llamadas.AgregarEnviados(llamada);
                return true;
            }
            
            case Precio precio:
            {
                registro.RegistrarPrecio(precio);
                return true;
            }

            case Reunion reunion:
            {
                registro.Reunion = reunion;
                return true;
            }

            case Venta venta:
            {
                registro.Ventas.AgregarVenta(venta);
                return true;
            }

            default:
            {
                return false;
            }
        }
    }
    
    public bool RegistrarMensajeRecibido(int clienteId, Mensaje mensaje)
    {
        var registro = BuscarClientePorId(clienteId);
        if (registro == null || mensaje == null) return false;

        registro.Mensajes.AgregarRecibidos(mensaje, mensaje.Fecha);
        return true;
    }
    
    public bool RegistrarLlamadaRecibida(int clienteId, Llamada llamada)
    {
        var registro = BuscarClientePorId(clienteId);
        if (registro == null || llamada == null) return false;

        registro.Llamadas.AgregarRecibidos(llamada);
        return true;
    }
    
    public int TotalVentasPorPeriodo(int clienteId, DateTime desde, DateTime hasta)
    {
        var registro = BuscarClientePorId(clienteId);
        if (registro == null)
            return 0;

        int total = 0;

        foreach (var venta in registro.Ventas.ListaVentas)
        {
            DateTime fechaVenta;

            // Intentamos convertir la fecha de la venta a DateTime
            bool conversionExitosa = DateTime.TryParse(venta.Fecha, out fechaVenta);

            if (conversionExitosa)
            {
                if (fechaVenta >= desde && fechaVenta <= hasta)
                {
                    total += venta.Precio;
                }
            }
            // Si la conversión falla, simplemente ignoramos esa venta
        }

        return total;
    }

    
    public List<Cliente> BuscarClientes(string nombre = null, string apellido = null, string telefono = null, string email = null)
    {
        List<Cliente> resultados = new List<Cliente>();

        foreach (var registro in Clientes)
        {
            Cliente cliente = registro.Cliente;
            bool coincide = true;

            if (nombre != null && !cliente.Nombre.ToLower().Contains(nombre.ToLower()))
            {
                coincide = false;
            }

            if (apellido != null && !cliente.Apellido.ToLower().Contains(apellido.ToLower()))
            {
                coincide = false;
            }

            if (telefono != null && !cliente.Telefono.Contains(telefono))
            {
                coincide = false;
            }

            if (email != null && !cliente.Email.ToLower().Contains(email.ToLower()))
            {
                coincide = false;
            }

            if (coincide)
            {
                resultados.Add(cliente);
            }
        }

        return resultados;
    }
    
    public bool AgregarDescripcionALlamada(int clienteId, Llamada llamada, string descripcion)
    {
        var registro = BuscarClientePorId(clienteId);
        if (registro == null || llamada == null || string.IsNullOrWhiteSpace(descripcion)) return false;

        if (registro.Llamadas.Enviados.Contains(llamada) || registro.Llamadas.Recibidos.Contains(llamada))
        {
            llamada.Descripcion = descripcion;
            return true;
        }

        return false;
    }
    
    public bool AgregarDescripcionAMensaje(int clienteId, Mensaje mensaje, string descripcion)
    {
        var registro = BuscarClientePorId(clienteId);
        if (registro == null || mensaje == null || string.IsNullOrWhiteSpace(descripcion)) return false;

        if (registro.Mensajes.mensajesEnviados.Contains(mensaje) || registro.Mensajes.mensajesRecibidos.Contains(mensaje))
        {
            mensaje.Texto += " - " + descripcion;
            return true;
        }

        return false;
    }
    
    public bool AgregarDescripcionAReunion(int clienteId, Reunion reunion, string descripcion)
    {
        var registro = BuscarClientePorId(clienteId);
        if (registro == null || reunion == null || string.IsNullOrWhiteSpace(descripcion)) return false;

        if (registro.Reunion == reunion)
        {
            reunion.Descripcion = descripcion;
            return true;
        }

        return false;
    }
    
    public bool AgregarDescripcionAEmail(int clienteId, Email email, string descripcion)
    {
        var registro = BuscarClientePorId(clienteId);
        if (registro == null || email == null || string.IsNullOrWhiteSpace(descripcion)) return false;

        if (registro.Emails.Enviados.Contains(email) || registro.Emails.Recibidos.Contains(email))
        {
            email.Descripcion = descripcion;
            return true;
        }

        return false;
    }

}