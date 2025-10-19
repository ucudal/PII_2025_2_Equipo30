namespace Library.Clases_tipos;

public class Emails
{
    public List<Email> Enviados { get; set; }
    public List<Email> Recibos { get; set; }
    public string Asunto { get; set; }
    public string Descripcion { get; set; }

    public void AgregarRecibidos(Email email)
    {
        //TODO
    }

    public void AgregarEnviados(Email email)
    {
        //TODO
    }

    public void AgregarAsunto(string asunto)
    {
        //TODO
    }

    public void AgregarDescripcion(string descripcion)
    {
        //TODO
    }
}