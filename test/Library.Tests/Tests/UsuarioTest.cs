using System.Runtime.CompilerServices;
using Library.Clases_principales;

namespace Library.Tests;

public class UsuarioTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ModificarCliente_DeberiaCambiarDatosDelCliente()
    {
        Usuario usuario = new Usuario { Nombre = "Pedro", Clave = "1234" };
        Cliente cliente = new Cliente("Juan", "Perez", "099123456", "juana@mail.com") { Id = 1 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        bool modificado = usuario.ModificarCliente(1, nuevoNombre: "Juana");
        
        Assert.That(modificado.Equals(true));
        Assert.That(usuario.Clientes[0].Cliente.Nombre.Equals("Juana"));
    }
    
    [Test]
    public void BuscarClientePorId_DeberiaRetornarClienteCorrecto()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Pepito", "Pepitez", "099123456", "altomail@mail.com") { Id = 5 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        var resultado = usuario.BuscarClientePorId(5);

        Assert.That(resultado, Is.Not.Null);
        Assert.That(5.Equals(resultado.Cliente.Id));
    }

    [Test]
    public void EliminarCliente_DeberiaEliminarClienteExistente()
    {
        Usuario usuario = new Usuario();
        Cliente cliente = new Cliente("Emmanuel", "Aristov", "096242460", "earis4499@mail.com") { Id = 10 };
        usuario.Clientes.Add(new RegistroCliente(cliente));

        bool eliminado = usuario.EliminarCliente(cliente);

        Assert.That(eliminado.Equals(true));
        Assert.That(usuario.Clientes, Is.Empty );
    }
}
