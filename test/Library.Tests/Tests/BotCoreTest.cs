using System.Collections.Generic;
using Library.BotCore;
using Library.BotCore.Interfaces;
using NUnit.Framework;

namespace Library.Tests;

/// <summary>
/// Pruebas unitarias para la clase <see cref="BotCore"/>.
/// </summary>
/// <remarks>
/// Se utilizan comandos y contextos falsos para validar el registro,
/// listado y ejecución de comandos en el bot.
/// </remarks>
public class BotCoreTest
{
    private BotCore.BotCore _bot;

    /// <summary>
    /// Comando falso que implementa <see cref="IBotCommand"/>.
    /// </summary>
    private class FakeCommand : IBotCommand
    {
        public string Nombre { get; } = "Comando de prueba";
        public string Descripcion { get; } = "Descripción de prueba";
        public bool Ejecutado { get; private set; } = false;

        public bool Ejecutar(IMessageContext contexto)
        {
            Ejecutado = true;
            contexto.EnviarMensaje("✅ Comando ejecutado correctamente.");
            return true;
        }
    }

    /// <summary>
    /// Contexto falso que captura los mensajes enviados por el bot.
    /// </summary>
    private class FakeMessageContext : IMessageContext
    {
        public List<string> Mensajes { get; } = new List<string>();

        public string UsuarioId { get; }
        public string UsuarioNombre { get; }

        public void EnviarMensaje(string mensaje)
        {
            Mensajes.Add(mensaje);
        }

        public string EsperarRespuesta()
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="BotCore"/> antes de cada prueba.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _bot = new BotCore.BotCore();
    }

    /// <summary>
    /// Verifica que al registrar un comando, este aparezca en la lista de comandos.
    /// </summary>
    [Test]
    public void RegistrarComando()
    {
        var comando = new FakeCommand();
        _bot.RegistrarComando(comando);

        string lista = _bot.MostrarComandos();

        Assert.That(lista.Contains("Comando de prueba"), Is.True);
    }

    /// <summary>
    /// Verifica que al procesar una opción válida, el comando se ejecute correctamente.
    /// </summary>
    [Test]
    public void ProcesarOpcion_Valida()
    {
        var comando = new FakeCommand();
        var contexto = new FakeMessageContext();
        _bot.RegistrarComando(comando);

        bool resultado = _bot.ProcesarOpcion("1", contexto);

        Assert.That(resultado, Is.True);
        Assert.That(comando.Ejecutado, Is.True);
        Assert.That(contexto.Mensajes.Contains("✅ Comando ejecutado correctamente."), Is.True);
    }

    /// <summary>
    /// Verifica que al procesar una opción inexistente, se muestre un mensaje de error.
    /// </summary>
    [Test]
    public void ProcesarOpcion_Inexistente()
    {
        var contexto = new FakeMessageContext();
        bool resultado = _bot.ProcesarOpcion("99", contexto);

        Assert.That(resultado, Is.False);
        Assert.That(contexto.Mensajes.Contains("El comando seleccionado no existe."), Is.True);
    }

    /// <summary>
    /// Verifica que al ingresar un texto no numérico, se muestre un mensaje de error.
    /// </summary>
    [Test]
    public void ProcesarOpcion_FormatoIncorrecto()
    {
        var contexto = new FakeMessageContext();
        bool resultado = _bot.ProcesarOpcion("abc", contexto);

        Assert.That(resultado, Is.False);
        Assert.That(contexto.Mensajes.Contains("El formato es incorrecto, porfavor, indique el comando con un numero."), Is.True);
    }
}