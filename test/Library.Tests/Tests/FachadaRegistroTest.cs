using Library.Clases_principales;
using Library.Clases_tipos;
using Library.Fachadas;

namespace Library.Tests;

/// <summary>
/// Conjunto de pruebas unitarias para la clase <c>FachadaRegistro</c>.
/// Se evalúan las funcionalidades relacionadas con la creación,
/// eliminación y gestión de usuarios y clientes.
/// </summary>
public class FachadaRegistroTest
{
    /// <summary>
    /// Método ejecutado antes de cada prueba.
    /// Actualmente no realiza ninguna configuración previa.
    /// </summary>
    [SetUp]
    public void Setup()
    {
    }
    
    /// <summary>
    /// Prueba que verifica la creación de un nuevo usuario a través de la fachada.
    /// 
    /// Valida que:
    /// - El usuario creado no sea nulo.
    /// - El nombre asignado coincida con el proporcionado.
    /// </summary>
    [Test]
    public void CrearUsuario()
    {
        var fachada = new FachadaRegistro();
        var usuario = fachada.CrearUsuario("nuevoUsuario", "clave123");

        Assert.That(usuario, Is.Not.Null);
        Assert.That(usuario.Nombre, Is.EqualTo("nuevoUsuario"));
    }
    
    /// <summary>
    /// Prueba que verifica la eliminación correcta de un usuario existente.
    /// 
    /// Valida que:
    /// - El método retorne <c>true</c> al eliminar un usuario previamente creado.
    /// </summary>
    [Test]
    public void EliminarUsuario()
    {
        var fachada = new FachadaRegistro();
        fachada.CrearUsuario("usuarioEliminar", "clave");

        var eliminado = fachada.EliminarUsuario("usuarioEliminar");

        Assert.That(eliminado, Is.True);
    }

    /// <summary>
    /// Prueba que verifica el comportamiento del método <c>EliminarUsuario</c>
    /// cuando se intenta eliminar un usuario que no existe.
    /// 
    /// Debe retornar <c>false</c>.
    /// </summary>
    [Test]
    public void EliminarUsuario_SiNoExiste()
    {
        var fachada = new FachadaRegistro();

        var eliminado = fachada.EliminarUsuario("noExiste");

        Assert.That(eliminado, Is.False);
    }
    
    /// <summary>
    /// Prueba que verifica la creación de un cliente asociado a un usuario
    /// mediante la fachada.
    /// 
    /// Valida que:
    /// - El cliente se cree correctamente.
    /// - El usuario tenga exactamente un cliente registrado.
    /// - Los datos del cliente coincidan con los ingresados.
    /// </summary>
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
    
    /// <summary>
    /// Prueba que verifica el cálculo del total de ventas de un cliente
    /// en un período determinado mediante la fachada.
    /// 
    /// Valida que:
    /// - Se sumen correctamente las ventas del cliente dentro del rango de fechas.
    /// - El total retornado coincida con la suma esperada.
    /// </summary>
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
