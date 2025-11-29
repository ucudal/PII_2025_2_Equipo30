using Library.Clases_tipos;

namespace Library.Clases_principales;
/// <summary>
/// Clase para integrar en comando de panel resumen.
/// </summary>
public class PanelResumen
{
    /// <summary>
    /// Representa el número total de clientes.
    /// </summary>
    public int TotalClientes { get; set; }
    /// <summary>
    /// Representa las interacciones recientes.
    /// </summary>
    public List<string> InteraccionesRecientes { get; set; } = new List<string>();
    /// <summary>
    /// Representa las reuniones proximas. 
    /// </summary>
    public List<Reunion> ReunionesProximas { get; set; } = new List<Reunion>();
}
