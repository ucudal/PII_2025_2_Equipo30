using System.Globalization;
using Library.BotCore.Interfaces;
using Library.Clases_principales;
using Library.Fachadas;

namespace Library.BotCore.Comandos;
/// <summary>
/// Genera el Promedio de Ventas de un Usuario.
/// </summary>
public class PromedioVentasUsuario : IBotCommand
{
    public string Nombre { get; } = "Promedio de Ventas";
    public string Descripcion { get; }
    private BotCore _bot;
    private FachadaRegistro _fachada;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fachada">Instancia FachadaRegistro.</param>
    /// <param name="bot">Instancia BotCore.</param>
    public PromedioVentasUsuario(BotCore bot, FachadaRegistro fachada)
    {
        _bot = bot;
        _fachada = fachada;
    }
    /// <summary>
    /// Ejecuta el generador de promedios.
    /// </summary>
    /// <param name="contexto"></param>
    /// <returns></returns>
    public bool Ejecutar(IMessageContext contexto)
    {
        if (!_bot.Sesion.EstaLogeado || _bot.Sesion.Rol != "Usuario")
        {
            contexto.EnviarMensaje("❌ Solo un usuario puede listar promedios de venta.");
            return false;
        }

        if (_bot.Sesion.UsuarioActual is Usuario usuario)
        {
            DateTime promedioInicio;
            DateTime promedioFin;
            //Solicitar fechas
            contexto.EnviarMensaje($"Ingrese la fecha de inicio para calcular el periodo de ventas (DD/MM/AA):");
            if (DateTime.TryParseExact(contexto.EsperarRespuesta(), "dd/MM/yy", null, DateTimeStyles.None,
                    out promedioInicio))
            {
                contexto.EnviarMensaje($"Ingrese la fecha de fin para calcular el periodo de ventas (DD/MM/AA):");
                if (DateTime.TryParseExact(contexto.EsperarRespuesta(), "dd/MM/yy", null, DateTimeStyles.None,
                        out promedioFin))
                {
                    int ventasTotales = 0;
                    int dineroTotal = 0;
                    //Calcular promedio
                    foreach (var c in usuario.Clientes)
                    {
                        foreach (var venta in c.Ventas.ListaVentas)
                        {
                            if (venta.Precio != null)
                            {
                                ventasTotales++;
                                dineroTotal += venta.Precio;
                            }
                        }
                    }
                    contexto.EnviarMensaje($"En el periodo dado de {promedioInicio} hasta {promedioFin} se regitraron un total de {ventasTotales} con una cantidad de {dineroTotal}$ en ventas (sin descontar gastos).");
                    return true;
                }
                else
                {
                    contexto.EnviarMensaje("Ingreso un formato de fecha invalido.");
                    return false;
                }
            }
            else
            {
                contexto.EnviarMensaje("Ingresó un formato de fecha invalido.");
                return false;
            }
        }
        contexto.EnviarMensaje("Hubo algun error.");
        return false;
    }
}