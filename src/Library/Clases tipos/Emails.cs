namespace Library.Clases_tipos;

public class Emails
{
    public List<Email> Enviados { get; set; }
    public List<Email> Recibos { get; set; }
    public string Asunto { get; set; }
    public string Descripcion { get; set; }

    public void AgregarRecibidos(Email email)
    {
        //Logica
    }

    public void AgregarEnviados(Email email)
    {
        //Logica
    }

    public void AgregarAsunto(string asunto)
    {
        //Logica
    }

    public void AgregarDescripcion(string descripcion)
    {
        //Logica
    }
}