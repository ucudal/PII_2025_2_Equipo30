namespace Library.Clases_tipos;

public class Mensajes
{
    public List<Mensaje> mensajesEnviados { get; set; }
    public List<Mensaje> mensajesRecibidos { get; set; }
    public string Asunto { get; set; }
    public string Descripcion { get; set; }

    public void AgregarRecibidos(Mensaje mensaje, string fecha)
    {
        this.mensajesRecibidos.Add(mensaje);
    }

    public void AgregarEnviados(Mensaje mensaje)
    {
        this.mensajesEnviados.Add(mensaje);
    }

    public void AgregarAsunto(string asunto)
    {
        this.Asunto = asunto;
    }

    public void AgregarDescripcion(string descripcion)
    {
        this.Descripcion = descripcion;
    }
}