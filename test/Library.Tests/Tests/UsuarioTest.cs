using System.Runtime.CompilerServices;
using Library.Clases_principales;
using Library.Clases_tipos;

namespace Library.Tests;

public class UsuarioTests
{
    [SetUp]
    public void Setup()
    {
    }

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
    
    [Test]
    public void ActualizarCliente_ConMensaje()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Ana", "Lopez", "099111222", "ana@mail.com") { Id = 1 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Mensaje mensaje = new Mensaje { Fecha = "2025-11-08", Texto = "Hola Ana" };
        bool resultado = usuario.ActualizarCliente(1, mensaje);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Mensajes.mensajesEnviados, Contains.Item(mensaje));
    }
    
    [Test]
    public void ActualizarCliente_ConLlamada()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Luis", "Martinez", "098765432", "luis@mail.com") { Id = 2 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Llamada llamada = new Llamada { Fecha = "2025-11-08", Asunto = "Consulta de precios" };
        bool resultado = usuario.ActualizarCliente(2, llamada);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Llamadas.Enviados, Contains.Item(llamada));
    }
    
    [Test]
    public void ActualizarCliente_ConPrecio()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Carlos", "Gomez", "097654321", "carlos@mail.com") { Id = 3 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Precio precio = new Precio { Fecha = "2025-11-08", Descripcion = "Cotización básica", Costo = 1500 };
        bool resultado = usuario.ActualizarCliente(3, precio);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Precio, Is.EqualTo(precio));
    }

    [Test]
    public void ActualizarCliente_ConReunion()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Sofia", "Fernandez", "096543210", "sofia@mail.com") { Id = 4 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Reunion reunion = new Reunion { Fecha = "2025-11-10", Lugar = "Oficina Central", Asunto = "Presentación de producto" };
        bool resultado = usuario.ActualizarCliente(4, reunion);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Reunion, Is.EqualTo(reunion));
    }

    [Test]
    public void ActualizarCliente_ConVenta()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Lucia", "Rodriguez", "095432109", "lucia@mail.com") { Id = 5 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Venta venta = new Venta { Fecha = "2025-11-08", Descripcion = "Servicio Premium", Precio = 2500 };
        bool resultado = usuario.ActualizarCliente(5, venta);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Ventas.ListaVentas, Contains.Item(venta));
    }

}
