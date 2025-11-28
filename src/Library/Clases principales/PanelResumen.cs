using Library.Clases_tipos;

namespace Library.Clases_principales;

public class PanelResumen
{
    public int TotalClientes { get; set; }
    public List<string> InteraccionesRecientes { get; set; } = new List<string>();
    public List<Reunion> ReunionesProximas { get; set; } = new List<Reunion>();
}
