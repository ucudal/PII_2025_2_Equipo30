namespace Library.Clases_tipos;

public class Mensajes
{
    public List<Mensaje> mensajesEnviados { get; set; }
    public List<Mensaje> mensajesRecibidos { get; set; }
    public string Asunto { get; set; }
    public string Descripcion { get; set; }

    public void AgregarRecibidos(string mensaje, string fecha)
    {
        //TODO
    }

    public void AgregarEnviados(Mensaje mensaje)
    {
        //TODO
    }

    public void AgregarAsunto(Mensaje mensaje)
    {
        //TODO
    }

    public void AgregarDescripcion(string descripcion)
    {
        //TODO
    }
    
    //TODO ¿Constructor?
}