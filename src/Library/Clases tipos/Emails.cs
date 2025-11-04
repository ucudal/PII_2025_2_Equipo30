namespace Library.Clases_tipos;

public class Emails
{
    public List<Email> Enviados { get; set; }
    public List<Email> Recibidos { get; set; }
    public string Asunto { get; set; }
    public string Descripcion { get; set; }

    public void AgregarRecibidos(Email email)
    {
        this.Recibidos.Add(email);
    }

    public void AgregarEnviados(Email email)
    {
        this.Enviados.Add(email);
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