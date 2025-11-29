using System.Runtime.CompilerServices;
using Library.Clases_principales;
using Library.Clases_tipos;

namespace Library.Tests;

public class UsuarioTests
{
    /// <summary>
    /// Método ejecutado antes de cada prueba.
    /// Actualmente no realiza ninguna inicialización específica.
    /// </summary>
    [SetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Prueba la modificación de los datos de un cliente existente.
    /// Verifica que:
    /// - El método retorne true.
    /// - El nombre del cliente sea actualizado correctamente.
    /// </summary>
    [Test]
    public void ModificarCliente()
    {
        Usuario usuario = new Usuario { Nombre = "Pedro", Clave = "1234" };
        Cliente cliente = new Cliente("Juan", "Perez", "099123456", "juana@mail.com") { Id = 1 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        bool modificado = usuario.ModificarCliente(1, nuevoNombre: "Juana");
        
        Assert.That(modificado.Equals(true));
        Assert.That(usuario.Clientes[0].Cliente.Nombre.Equals("Juana"));
    }
    
    /// <summary>
    /// Prueba la búsqueda de un cliente por su ID.
    /// Verifica que:
    /// - Se encuentre el cliente.
    /// - El ID coincida con el esperado.
    /// </summary>
    [Test]
    public void BuscarClientePorId()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Pepito", "Pepitez", "099123456", "altomail@mail.com") { Id = 5 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        var resultado = usuario.BuscarClientePorId(5);

        Assert.That(resultado, Is.Not.Null);
        Assert.That(5.Equals(resultado.Cliente.Id));
    }

    /// <summary>
    /// Prueba eliminar un cliente de la lista del usuario.
    /// Verifica que:
    /// - El método retorne true.
    /// - La lista quede vacía.
    /// </summary>
    [Test]
    public void EliminarCliente()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Emmanuel", "Aristov", "096242460", "earis4499@mail.com") { Id = 10 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        bool eliminado = usuario.EliminarCliente(cliente);

        Assert.That(eliminado.Equals(true));
        Assert.That(usuario.Clientes, Is.Empty );
    }
    
    /// <summary>
    /// Prueba actualizar un cliente agregando un mensaje enviado.
    /// Verifica que quede registrado correctamente.
    /// </summary>
    [Test]
    public void ActualizarCliente_ConMensaje()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Ana", "Lopez", "099111222", "ana@mail.com") { Id = 1 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Mensaje mensaje = new Mensaje { Fecha = "17/06/2025", Texto = "Hola Ana" };
        bool resultado = usuario.ActualizarCliente(1, mensaje);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Mensajes.mensajesEnviados, Contains.Item(mensaje));
    }
    
    /// <summary>
    /// Prueba registrar un mensaje recibido para un cliente.
    /// Verifica que se agregue correctamente a la lista de mensajes recibidos.
    /// </summary>
    [Test]
    public void RegistrarMensajeRecibido()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Diego", "Silva", "097654321", "diego@mail.com") { Id = 8 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Mensaje mensaje = new Mensaje { Fecha = "09/11/2025", Texto = "Consulta por producto" };
        bool resultado = usuario.RegistrarMensajeRecibido(8, mensaje);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Mensajes.mensajesRecibidos, Contains.Item(mensaje));
    }

    /// <summary>
    /// Prueba actualizar un cliente agregando una llamada enviada.
    /// </summary>
    [Test]
    public void ActualizarCliente_ConLlamada()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Luis", "Martinez", "098765432", "luis@mail.com") { Id = 2 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Llamada llamada = new Llamada { Fecha = "21/04/2025", Asunto = "Consulta de precios" };
        bool resultado = usuario.ActualizarCliente(2, llamada);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Llamadas.Enviados, Contains.Item(llamada));
    }
    
    /// <summary>
    /// Prueba registrar una llamada recibida por un cliente.
    /// </summary>
    [Test]
    public void RegistrarLlamadaRecibida()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Laura", "Torres", "098765432", "laura@mail.com") { Id = 7 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Llamada llamada = new Llamada { Fecha = "10/11/2025", Asunto = "Recibida: Reclamo técnico" };
        bool resultado = usuario.RegistrarLlamadaRecibida(7, llamada);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Llamadas.Recibidos, Contains.Item(llamada));
    }

    /// <summary>
    /// Prueba actualizar un cliente agregando un precio/cotización.
    /// </summary>
    [Test]
    public void ActualizarCliente_ConPrecio()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Carlos", "Gomez", "097654321", "carlos@mail.com") { Id = 3 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Precio precio = new Precio { Fecha = "03/08/2025", Descripcion = "Cotización básica", Costo = 1500 };
        bool resultado = usuario.ActualizarCliente(3, precio);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Precio, Is.EqualTo(precio));
    }

    /// <summary>
    /// Prueba actualizar un cliente agregando una reunión.
    /// </summary>
    [Test]
    public void ActualizarCliente_ConReunion()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Sofia", "Fernandez", "096543210", "sofia@mail.com") { Id = 4 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Reunion reunion = new Reunion { Fecha = "02/12/2025", Lugar = "Oficina Central", Asunto = "Presentación de producto" };
        bool resultado = usuario.ActualizarCliente(4, reunion);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Reunion, Is.EqualTo(reunion));
    }

    /// <summary>
    /// Prueba actualizar un cliente agregando una venta.
    /// </summary>
    [Test]
    public void ActualizarCliente_ConVenta()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Lucia", "Rodriguez", "095432109", "lucia@mail.com") { Id = 5 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Venta venta = new Venta { Fecha = "18/01/2025", Descripcion = "Servicio Premium", Precio = 2500 };
        bool resultado = usuario.ActualizarCliente(5, venta);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Ventas.ListaVentas, Contains.Item(venta));
    }

    /// <summary>
    /// Prueba calcular el total de ventas de un cliente dentro de un período de tiempo.
    /// </summary>
    [Test]
    public void TotalVentasPorPeriodo()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Clara", "Mendez", "091122334", "clara@mail.com") { Id = 10 };
        var registro = new RegistroCliente(cliente);
        registro.Ventas.AgregarVenta(new Venta { Fecha = "01/11/2025", Descripcion = "Pack A", Precio = 1000 });
        registro.Ventas.AgregarVenta(new Venta { Fecha = "05/11/2025", Descripcion = "Pack B", Precio = 1500 });
        usuario.Clientes.Add(registro);

        int total = usuario.TotalVentasPorPeriodo(10, new DateTime(2025, 11, 01), new DateTime(2025, 11, 06));

        Assert.That(total, Is.EqualTo(2500));
    }
    
    /// <summary>
    /// Prueba agregar una etiqueta personalizada a un cliente.
    /// </summary>
    [Test]
    public void AgregarEtiqueta()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Mario", "Bros", "091234567", "mario@mail.com") { Id = 9 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        bool resultado = usuario.AgregarEtiqueta(9, "Fidelizado");

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Cliente.Etiqueta, Is.EqualTo("Fidelizado"));
    }
    
    /// <summary>
    /// Prueba agregar una descripción adicional a una llamada enviada.
    /// </summary>
    [Test]
    public void AgregarDescripcionALlamada()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Laura", "Torres", "098765432", "laura@mail.com") { Id = 11 };
        var llamada = new Llamada { Fecha = "10/11/2025", Asunto = "Consulta técnica" };
        var registro = new RegistroCliente(cliente);
        registro.Llamadas.AgregarEnviados(llamada);
        usuario.Clientes.Add(registro);

        bool resultado = usuario.AgregarDescripcionALlamada(11, llamada, "Cliente tenía dudas sobre el servicio premium");

        Assert.That(resultado, Is.True);
        Assert.That(llamada.Descripcion, Is.EqualTo("Cliente tenía dudas sobre el servicio premium"));
    }

    /// <summary>
    /// Prueba agregar una descripción adicional a un mensaje ya existente.
    /// </summary>
    [Test]
    public void AgregarDescripcionAMensaje()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Diego", "Silva", "097654321", "diego@mail.com") { Id = 12 };
        var mensaje = new Mensaje { Fecha = "09/11/2025", Texto = "Consulta por producto" };
        var registro = new RegistroCliente(cliente);
        registro.Mensajes.AgregarEnviados(mensaje);
        usuario.Clientes.Add(registro);

        bool resultado = usuario.AgregarDescripcionAMensaje(12, mensaje, "Mensaje relacionado a cotización");

        Assert.That(resultado, Is.True);
        Assert.That(mensaje.Texto, Does.Contain("Mensaje relacionado a cotización"));
    }
    
    /// <summary>
    /// Prueba agregar una descripción adicional a una reunión.
    /// </summary>
    [Test]
    public void AgregarDescripcionAReunion()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Sofia", "Fernandez", "096543210", "sofia@mail.com") { Id = 13 };
        var reunion = new Reunion { Fecha = "02/12/2025", Lugar = "Oficina Central", Asunto = "Presentación de producto" };
        var registro = new RegistroCliente(cliente);
        registro.Reunion = reunion;
        usuario.Clientes.Add(registro);

        bool resultado = usuario.AgregarDescripcionAReunion(13, reunion, "Se presentó el nuevo catálogo de productos");

        Assert.That(resultado, Is.True);
        Assert.That(reunion.Descripcion, Is.EqualTo("Se presentó el nuevo catálogo de productos"));
    }
    
    /// <summary>
    /// Prueba agregar una descripción adicional a un email enviado.
    /// </summary>
    [Test]
    public void AgregarDescripcionAEmail()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Clara", "Mendez", "091122334", "clara@mail.com") { Id = 14 };
        var email = new Email { Fecha = "05/11/2025", Texto = "Cotización enviada" };
        var registro = new RegistroCliente(cliente);
        registro.Emails.AgregarEnviados(email);
        usuario.Clientes.Add(registro);

        bool resultado = usuario.AgregarDescripcionAEmail(14, email, "Incluye detalles del paquete premium");

        Assert.That(resultado, Is.True);
        Assert.That(email.Descripcion, Is.EqualTo("Incluye detalles del paquete premium"));
    }

}
