namespace Library.Clases_tipos;

public class Mensajes
{
    public List<Mensaje> mensajesEnviados { get; set; }
    public List<Mensaje> mensajesRecibidos { get; set; }
    public string Asunto { get; set; }
    public string Descripcion { get; set; }

    public void AgregarRecibidos(string mensaje, string fecha)
    {
        //Logica
    }

    public void AgregarEnviados(Mensaje mensaje)
    {
        //Logica
    }

    public void AgregarAsunto(Mensaje mensaje)
    {
        //Logica
    }

    public void AgregarDescripcion(string descripcion)
    {
        //Logica
    }
    
    //Esqueleto general
}