using Library.Clases_principales;
using Library.Clases_tipos;

namespace Library.Tests;

public class RegistroClientesTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void RegistrarGenero()
    {
        Cliente cliente = new Cliente("Joe", "Doe", "999999999", "JoeDoe@mail.com");
        RegistroCliente registro = new RegistroCliente(cliente);

        bool resultado = registro.RegistrarGenero("Femenino");

        Assert.That(resultado.Equals(true));
        Assert.That(cliente.Genero.Equals("Femenino"));
    }

    [Test]
    public void RegistrarNacimiento()
    {
        Cliente cliente = new Cliente("Juan", "Perez", "099888888", "juan@mail.com");
        RegistroCliente registro = new RegistroCliente(cliente);

        bool resultado = registro.RegistrarNacimiento("");

        Assert.That(resultado.Equals(false));
    }
    
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
