using Library.Clases_principales;

namespace Library.Tests;

/// <summary>
/// Conjunto de pruebas unitarias para la clase <c>Administrador</c>.
/// Se validan las operaciones de creación y eliminación de usuarios.
/// </summary>
public class AdministradorTest
{
    /// <summary>
    /// Método ejecutado antes de cada prueba.
    /// Actualmente no realiza acciones previas.
    /// </summary>
    [SetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Prueba que verifica que un administrador pueda crear un nuevo usuario
    /// correctamente mediante el método <c>CrearUsuario</c>.
    /// 
    /// Valida que el usuario creado sea agregado a la lista de usuarios del administrador.
    /// </summary>
    [Test]
    public void CrearUsuario()
    {
        Administrador administrador = new Administrador("admin", "1234");
        Usuario usuario = administrador.CrearUsuario("tiendaropa", "constraseña");
        
        Assert.That(administrador.Usuarios, Contains.Item(usuario));
    }

    /// <summary>
    /// Prueba que verifica la eliminación de un usuario existente.
    /// 
    /// Valida que el método <c>EliminarUsuario</c> retorne <c>true</c>
    /// cuando el usuario a eliminar sí existe.
    /// </summary>
    [Test]
    public void EliminarUsuario()
    {
        Administrador administrador=new Administrador("admin", "1234");
        administrador.CrearUsuario("Jorge", "ElMejorProfe");

        bool eliminado = administrador.EliminarUsuario(("Jorge"));
        
        Assert.That(eliminado.Equals(true));
    }

    /// <summary>
    /// Prueba que verifica el comportamiento del método <c>EliminarUsuario</c>
    /// cuando se intenta eliminar un usuario inexistente.
    /// 
    /// Debe retornar <c>false</c>.
    /// </summary>
    [Test]
    public void EliminarUsuario_SiNoExiste()
    {
        Administrador administrador = new Administrador("elpapu", "password");
        bool eliminado = administrador.EliminarUsuario("Momo Benavidez");
        
        Assert.That(eliminado.Equals(false));
    }
}
