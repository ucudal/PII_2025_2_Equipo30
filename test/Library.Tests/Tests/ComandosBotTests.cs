using Library.Fachadas;
using Library.Clases_principales;

namespace Library.Tests;

/// <summary>
/// Clase de pruebas para los comandos del bot.
/// </summary>
/// <remarks>
/// Esta clase inicializa la fachada y un usuario base en el método <see cref="Setup"/>.
/// Cada prueba valida un comando específico de la <see cref="FachadaRegistro"/>.
/// </remarks>
public class ComandosBotTest
{
    private FachadaRegistro _fachada;
    private Usuario _usuarioBase;

    /// <summary>
    /// Inicializa la fachada y crea un usuario base antes de cada prueba.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _fachada = Singleton<FachadaRegistro>.Instancia;
        _usuarioBase = _fachada.CrearUsuario("usuarioTest", "claveTest");
    }

    /// <summary>
    /// Prueba la creación de un nuevo usuario.
    /// </summary>
    /// <remarks>
    /// Se espera que el usuario creado pueda ser recuperado con <see cref="FachadaRegistro.LoginUsuario"/>.
    /// </remarks>
    [Test]
    public void CrearUsuario_DeberiaAgregarUsuarioALaLista()
    {
        Usuario usuario = _fachada.CrearUsuario("nuevoUsuario", "1234");
        Assert.That(_fachada.LoginUsuario("nuevoUsuario", "1234"), Is.EqualTo(usuario));
    }

    /// <summary>
    /// Prueba el inicio de sesión del administrador.
    /// </summary>
    /// <remarks>
    /// Se espera que el administrador predeterminado con credenciales "admin"/"admin" sea retornado correctamente.
    /// </remarks>
    [Test]
    public void LoginAdministrador_DeberiaRetornarAdminCorrecto()
    {
        Administrador admin = _fachada.LoginAdministrador("admin", "admin");
        Assert.That(admin, Is.Not.Null);
        Assert.That(admin.Nombre, Is.EqualTo("admin"));
    }

    /// <summary>
    /// Prueba la creación de un cliente asociado a un usuario.
    /// </summary>
    /// <remarks>
    /// Se espera que el cliente creado aparezca en la lista de clientes del usuario base.
    /// </remarks>
    [Test]
    public void CrearCliente_DeberiaAgregarClienteAlUsuario()
    {
        bool creado = _fachada.CrearCliente(_usuarioBase, "Juan", "Pérez", "099123456", "juan@test.com");
        Assert.That(creado, Is.True);
        Assert.That(_usuarioBase.Clientes.Any(c => c.Cliente.Nombre == "Juan" && c.Cliente.Apellido == "Pérez"), Is.True);
    }

    /// <summary>
    /// Prueba la suspensión de un usuario.
    /// </summary>
    /// <remarks>
    /// Se espera que el usuario quede marcado como suspendido tras ejecutar el comando.
    /// </remarks>
    [Test]
    public void SuspenderUsuario_DeberiaMarcarUsuarioComoSuspendido()
    {
        Usuario usuario = _fachada.CrearUsuario("suspendido", "clave");
        bool resultado = _fachada.SuspenderUsuario(usuario.Id);

        Assert.That(resultado, Is.True);
        Assert.That(usuario.Suspendido, Is.True);
    }

    /// <summary>
    /// Prueba la eliminación de un usuario por su identificador.
    /// </summary>
    /// <remarks>
    /// Se espera que el usuario sea eliminado de la lista y no pueda iniciar sesión posteriormente.
    /// </remarks>
    [Test]
    public void EliminarUsuarioPorId_DeberiaEliminarUsuarioExistente()
    {
        Usuario usuario = _fachada.CrearUsuario("paraEliminar", "clave");
        bool eliminado = _fachada.EliminarUsuarioPorId(usuario.Id);

        Assert.That(eliminado, Is.True);
        Assert.That(_fachada.LoginUsuario("paraEliminar", "clave"), Is.Null);
    }
}
