using Library.Clases_principales;
using Library.Clases_tipos;

namespace Library.Tests;

/// <summary>
/// Conjunto de pruebas unitarias para la clase <c>RegistroCliente</c>.
/// Valida el comportamiento de los métodos relacionados al registro
/// de información del cliente.
/// </summary>
public class RegistroClientesTests
{
    /// <summary>
    /// Método de inicialización ejecutado antes de cada test.
    /// Actualmente no realiza ninguna configuración adicional.
    /// </summary>
    [SetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Prueba que el método <c>RegistrarGenero</c> registre correctamente
    /// un género válido en el cliente.
    /// 
    /// Verifica que:
    /// - El método retorne <c>true</c>.
    /// - El atributo <c>Genero</c> del cliente coincida con el valor ingresado.
    /// </summary>
    [Test]
    public void RegistrarGenero()
    {
        Cliente cliente = new Cliente("Joe", "Doe", "999999999", "JoeDoe@mail.com");
        RegistroCliente registro = new RegistroCliente(cliente);

        bool resultado = registro.RegistrarGenero("Femenino");

        Assert.That(resultado.Equals(true));
        Assert.That(cliente.Genero.Equals("Femenino"));
    }

    /// <summary>
    /// Prueba que <c>RegistrarNacimiento</c> retorne <c>false</c> cuando se
    /// pasa una cadena vacía, indicando que no se ingresó una fecha válida.
    /// </summary>
    [Test]
    public void RegistrarNacimiento()
    {
        Cliente cliente = new Cliente("Juan", "Perez", "099888888", "juan@mail.com");
        RegistroCliente registro = new RegistroCliente(cliente);

        bool resultado = registro.RegistrarNacimiento("");

        Assert.That(resultado.Equals(false));
    }
    
    /// <summary>
    /// Prueba que el método <c>RegistrarPrecio</c> registre correctamente
    /// un objeto <c>Precio</c> asociado al registro del cliente.
    /// 
    /// Verifica que:
    /// - El método retorne <c>true</c>.
    /// - La propiedad <c>Precio</c> del registro coincida con el objeto entregado.
    /// </summary>
    [Test]
    public void RegistrarPrecio()
    {
        Cliente cliente = new Cliente("Nicolas", "Diaz", "092002027", "nosesumail@mail.com");
        RegistroCliente registro = new RegistroCliente(cliente);
        Precio precio = new Precio();

        bool resultado = registro.RegistrarPrecio(precio);

        Assert.That(resultado.Equals(true));
        Assert.That(registro.Precio.Equals(precio));
    }
}
