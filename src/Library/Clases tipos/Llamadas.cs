namespace Library.Clases_tipos;

public class Llamadas
{
    public List<Llamada> Enviados {get; set;}
    public List<Llamada> Recibidos {get; set;}
    public string Descripcion {get; set;}

    public void AgregarEnviados(Llamada llamada)
    {
        this.Enviados.Add(llamada);
    }

    public void AgregarRecibidos(Llamada llamada)
    {
        this.Recibidos.Add(llamada);
    }

    public void AgregarDescripcion(string descripcion)
    {
        this.Descripcion = descripcion;
    }
    
}