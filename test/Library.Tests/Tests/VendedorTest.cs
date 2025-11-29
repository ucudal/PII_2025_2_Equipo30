using Library.Clases_principales;
using Library.Clases_tipos;

namespace Library.Tests;

public class VendedorTests
{
    /// <summary>
    /// Configuración previa a cada test.
    /// Actualmente no realiza ninguna acción.
    /// </summary>
    [SetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Prueba que un cliente pueda ser reasignado correctamente desde un vendedor a otro.
    /// Verifica que:
    /// - El método devuelva true.
    /// - El cliente sea removido del primer vendedor.
    /// - El cliente sea agregado al segundo vendedor.
    /// </summary>
    [Test]
    public void AsignarClienteAotroVendedor()
    {
        Vendedor vendedor1 = new Vendedor("Juan", "1234");
        Vendedor vendedor2 = new Vendedor("Pedro", "abcd");

        Cliente cliente = new Cliente("Nombre", "Apellido", "091234567", "nombreapellido@mail.com") { Id = 1 };
        vendedor1.Clientes.Add(cliente);

        bool resultado = vendedor1.AsignarClienteAotroVendedor(cliente, vendedor2);

        Assert.That(resultado.Equals(true));
        Assert.That(vendedor1.Clientes, Is.Empty);
        Assert.That(vendedor2.Clientes, Has.Count.EqualTo(1));
        Assert.That(vendedor2.Clientes, Contains.Item(cliente));
    }

    /// <summary>
    /// Prueba que no se pueda reasignar un cliente que no pertenece al vendedor original.
    /// Verifica que:
    /// - El método devuelva false.
    /// </summary>
    [Test]
    public void AsignarClienteAotroVendedor_SiNoExiste()
    {
        Vendedor vendedor1 = new Vendedor("Juan", "1234");
        Vendedor vendedor2 = new Vendedor("Pedro", "abcd");

        Cliente cliente = new Cliente("Paraguay", "X", "000", "paraguay@gmail.com") { Id = 99 };

        bool resultado = vendedor1.AsignarClienteAotroVendedor(cliente, vendedor2);

        Assert.That(resultado.Equals(false));
    }

    /// <summary>
    /// Prueba la funcionalidad de listar clientes en un usuario.
    /// Verifica que:
    /// - La lista devuelta contenga la cantidad correcta.
    /// - Estén los clientes correspondientes.
    /// </summary>
    [Test]
    public void ListarClientes()
    {
        Usuario usuario = new Usuario();
        Cliente cliente1 = new Cliente("Ana", "Lopez", "099111222", "ana@mail.com") { Id = 1 };
        Cliente cliente2 = new Cliente("Luis", "Martinez", "098765432", "luis@mail.com") { Id = 2 };

        usuario.Clientes.Add(new RegistroCliente(cliente1));
        usuario.Clientes.Add(new RegistroCliente(cliente2));

        var lista = usuario.ListarClientes();

        Assert.That(lista, Has.Count.EqualTo(2));
        Assert.That(lista, Contains.Item(cliente1));
        Assert.That(lista, Contains.Item(cliente2));
    }

    /// <summary>
    /// Prueba actualizar un cliente agregándole una venta.
    /// Verifica que:
    /// - El método devuelva true.
    /// - La venta quede registrada correctamente en el cliente.
    /// </summary>
    [Test]
    public void ActualizarCliente_ConVenta()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Lucia", "Rodriguez", "095432109", "lucia@mail.com") { Id = 5 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Venta venta = new Venta { Fecha = "09/05/2025", Descripcion = "Servicio Premium", Precio = 2500 };
        bool resultado = usuario.ActualizarCliente(5, venta);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Ventas.ListaVentas, Contains.Item(venta));
    }

    /// <summary>
    /// Prueba modificar la etiqueta de un cliente según su ID.
    /// Verifica que:
    /// - El método devuelva true.
    /// - La etiqueta haya sido cambiada correctamente.
    /// </summary>
    [Test]
    public void ModificarCliente_Etiqueta()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Mario", "Bros", "091234567", "mario@mail.com") { Id = 6 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        bool resultado = usuario.ModificarCliente(6, nuevaEtiqueta: "VIP");

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Cliente.Etiqueta, Is.EqualTo("VIP"));
    }

    /// <summary>
    /// Prueba actualizar un cliente agregándole una reunión.
    /// Verifica que:
    /// - El método devuelva true.
    /// - La reunión quede registrada correctamente en el cliente.
    /// </summary>
    [Test]
    public void ActualizarCliente_ConReunion()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Sofia", "Fernandez", "096543210", "sofia@mail.com") { Id = 4 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        Reunion reunion = new Reunion { Fecha = "09/07/2025", Lugar = "Oficina Central", Asunto = "Presentación de producto" };
        bool resultado = usuario.ActualizarCliente(4, reunion);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Clientes[0].Reunion, Is.EqualTo(reunion));
    }
}
