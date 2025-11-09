namespace Library.Clases_tipos;
/// <summary>
/// Representa un conjunto de llamadas enviadas o recibidas con una descripción general.
/// </summary>
public class Llamadas
{
    /// <summary>
    /// Lista de llamadas realizadas (enviadas).
    /// </summary>
    public List<Llamada> Enviados {get; set;} = new List<Llamada>();
    /// <summary>
    /// Lista de llamadas recibidas.
    /// </summary>
    public List<Llamada> Recibidos {get; set;} = new List<Llamada>();
    /// <summary>
    /// Descripción o resumen general de las llamadas.
    /// </summary>
    public string Descripcion {get; set;}

    /// <summary>
    /// Agrega una llamada a la lista de llamadas enviadas.
    /// </summary>
    /// <param name="llamada">Instancia de la <see cref="Llamada"/> a agregar.</param>
    public void AgregarEnviados(Llamada llamada)
    {
        this.Enviados.Add(llamada);
    }

    /// <summary>
    /// Agrega una llamada a la lista de llamadas recibidas.
    /// </summary>
    /// <param name="llamada">Instancia de la <see cref="Llamada"/> a agregar.</param>
    public void AgregarRecibidos(Llamada llamada)
    {
        this.Recibidos.Add(llamada);
    }

    /// <summary>
    /// Define una descripción general para las llamadas.
    /// </summary>
    /// <param name="descripcion">Texto descriptivo de las llamadas.</param>
    public void AgregarDescripcion(string descripcion)
    {
        this.Descripcion = descripcion;
    }
    
}