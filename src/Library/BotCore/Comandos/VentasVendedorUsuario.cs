using System.Runtime.InteropServices.JavaScript;
using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;

//Utiliza la interfaz IBotCommand
public class VentasVendedorUsuario : IBotCommand
{
    //Nombre del comando, se va a listar en la lista de comandos del bot
    public string Nombre { get; } = "Ventas vendedor que superen x valor";
    public string Descripcion { get; }
    private BotCore _bot;
    private FachadaRegistro _fachada;

    public VentasVendedorUsuario(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    
    public bool Ejecutar(IMessageContext contexto)
    {
        //Si la entidad logueada no es un usuario, devuelve error y finaliza la ejecución.
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("No tiene sesión iniciada o no esta logueado como Usuario.");
            return false;
        }

        //Aún que en este caso no es necesario, utilizamos is Usuario usuario para tener la instancia del usuario logueado.
        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            
                contexto.EnviarMensaje($"Ingrese el monto a partir del cual quiere ver las ventas realizadas.");
                int montoVenta;
                //TryParse, si el usuario digita una letra, no entra en excepción.
                if (int.TryParse(contexto.EsperarRespuesta(), out montoVenta))
                {
                    contexto.EnviarMensaje("Lista de ventas:");
                    //Recorro lista de vendedores y para cada cliente del vendedor recorro sus ventas
                    foreach (var v in _fachada.Vendedores)
                    {
                        foreach (var c in v.RegistroClientes)
                        {
                            if (c.Ventas.ListaVentas != null)
                            {
                                foreach (var venta in c.Ventas.ListaVentas)
                                {
                                    if (venta.Precio > montoVenta)
                                    {
                                        contexto.EnviarMensaje($"Venta: {venta.Descripcion} vendido a {venta.Precio} el día {venta.Fecha} vendido por {v.Nombre} al cliente {c.Cliente.Nombre}");
                                    }
                                }
                            }
                        }
                    }
                    contexto.EnviarMensaje("Ya se listaron todas las ventas.");
                    return true;
                }
                else
                {
                    contexto.EnviarMensaje("Digitó un termino invalido, debe digitar un numero entero.");
                    return false;
                }
        }
        contexto.EnviarMensaje("Algo salió mal");
        return false;
    }
}