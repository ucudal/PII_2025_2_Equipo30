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
    
}