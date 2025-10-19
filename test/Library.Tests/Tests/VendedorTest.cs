using Library.Clases_principales;

namespace Library.Tests;

public class VendedorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void AsignarClienteAotroVendedor_DeberiaMoverClienteCorrectamente()
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

    [Test]
    public void AsignarClienteAotroVendedor_NoDebeAsignarSiClienteNoExiste()
    {
        Vendedor vendedor1 = new Vendedor("Juan", "1234");
        Vendedor vendedor2 = new Vendedor("Pedro", "abcd");

        Cliente cliente = new Cliente("Paraguay", "X", "000", "paraguay@gmail.com") { Id = 99 };

        bool resultado = vendedor1.AsignarClienteAotroVendedor(cliente, vendedor2);

        Assert.That(resultado.Equals(false));
    }
}

