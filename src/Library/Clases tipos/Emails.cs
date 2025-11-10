namespace Library.Clases_tipos;

/// <summary>
/// Representa un conjunto de correos electrónicos asociados a un cliente o usuario,
/// incluyendo los enviados y los recibidos.
/// </summary>
public class Emails
{
    public List<Email> Enviados { get; set; } = new List<Email>();
    public List<Email> Recibidos { get; set; } = new List<Email>();
    
    /// <summary>
    /// Asunto general del intercambio de correos.
    /// </summary>
    public string Asunto { get; set; }
    /// <summary>
    /// Descripción de los mails o del correo del cliente.
    /// </summary>
    public string Descripcion { get; set; }

    /// <summary>
    /// Agrega un nuevo correo a la lista de recibidos.
    /// </summary>
    /// <param name="email">Correo electrónico a agregar.</param>
    public void AgregarRecibidos(Email email)
    {
        this.Recibidos.Add(email);
    }

    /// <summary>
    /// Agrega un nuevo correo a la lista de enviados.
    /// </summary>
    /// <param name="email">Correo electrónico a agregar.</param>
    public void AgregarEnviados(Email email)
    {
        this.Enviados.Add(email);
    }

    /// <summary>
    /// Define el asunto de los correos.
    /// </summary>
    public void AgregarAsunto(string asunto)
    {
        this.Asunto = asunto;
    }

    /// <summary>
    /// Define una descripción de los mails o del correo del cliente.
    /// </summary>
    public void AgregarDescripcion(string descripcion)
    {
        this.Descripcion = descripcion;
    }
}