using Library.Clases_principales;

namespace Library.Tests;

public class AdministradorTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CrearUsuario()
    {
        Administrador administrador = new Administrador("admin", "1234");
        Usuario usuario = administrador.CrearUsuario("tiendaropa", "constraseña");
        
        Assert.That(administrador.Usuarios, Contains.Item(usuario));
    }

    [Test]
    public void EliminarUsuario()
    {
        Administrador administrador=new Administrador("admin", "1234");
        administrador.CrearUsuario("Jorge", "ElMejorProfe");

        bool eliminado = administrador.EliminarUsuario(("Jorge"));
        
        Assert.That(eliminado.Equals(true));
    }

    [Test]
    public void EliminarUsuario_SiNoExiste()
    {
        Administrador administrador = new Administrador("elpapu", "password");
        bool eliminado = administrador.EliminarUsuario("Momo Benavidez");
        
        Assert.That(eliminado.Equals(false));
    }
}
