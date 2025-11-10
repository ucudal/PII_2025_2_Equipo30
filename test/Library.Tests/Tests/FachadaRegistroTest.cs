using Library.Clases_principales;
using Library.Clases_tipos;
using Library.Fachadas;

namespace Library.Tests;

public class FachadaRegistroTest
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void CrearUsuario()
    {
        var fachada = new FachadaRegistro();
        var usuario = fachada.CrearUsuario("nuevoUsuario", "clave123");

        Assert.That(usuario, Is.Not.Null);
        Assert.That(usuario.Nombre, Is.EqualTo("nuevoUsuario"));
    }
    
    [Test]
    public void EliminarUsuario()
    {
        var fachada = new FachadaRegistro();
        fachada.CrearUsuario("usuarioEliminar", "clave");

        var eliminado = fachada.EliminarUsuario("usuarioEliminar");

        Assert.That(eliminado, Is.True);
    }

    [Test]
    public void EliminarUsuario_SiNoExiste()
    {
        var fachada = new FachadaRegistro();

        var eliminado = fachada.EliminarUsuario("noExiste");

        Assert.That(eliminado, Is.False);
    }
    
    [Test]
    public void CrearCliente_DesdeFachada()
    {
        var fachada = new FachadaRegistro();
        var usuario = fachada.CrearUsuario("usuario1", "clave");

        bool creado = fachada.CrearCliente(usuario, "Ana", "Lopez", "099111222", "ana@mail.com");

        Assert.That(creado, Is.True);
        Assert.That(usuario.Clientes.Count, Is.EqualTo(1));
        Assert.That(usuario.Clientes[0].Cliente.Nombre, Is.EqualTo("Ana"));
    }
    
    [Test]
    public void ObtenerTotalVentasPorPeriodo_DesdeFachada()
    {
        var fachada = new FachadaRegistro();
        var usuario = fachada.CrearUsuario("usuarioVentas", "clave");

        var cliente = new Cliente("Clara", "Mendez", "091122334", "clara@mail.com") { Id = 10 };
        var registro = new RegistroCliente(cliente);
        registro.Ventas.AgregarVenta(new Venta { Fecha = "01/11/2025", Descripcion = "Pack A", Precio = 1000 });
        registro.Ventas.AgregarVenta(new Venta { Fecha = "05/11/2025", Descripcion = "Pack B", Precio = 1500 });
        usuario.Clientes.Add(registro);

        int total = fachada.ObtenerTotalVentasPorPeriodo(usuario, 10, new DateTime(2025, 11, 01), new DateTime(2025, 11, 06));

        Assert.That(total, Is.EqualTo(2500));
    }

}