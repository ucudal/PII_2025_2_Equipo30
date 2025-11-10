namespace Library.Clases_principales;
/// <summary>
/// Contiene datos básicos del cliente.
/// </summary>
/// <remarks>
/// Solo contiene datos del cliente, no de su interacción con el <see cref="Usuario"/>.
/// </remarks>
public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido {get; set;}
    public string Telefono {get; set;}
    public string Email { get; set; }
    public string Genero { get; set; }
    public string FechaNacimiento { get; set; }
    public string Etiqueta {get; set;}

    public void Contactar()
    {
        // TODO: Este método se implementará en la fachada o subsistema de comunicación.
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="Nombre">Nombre del cliente.</param>
    /// <param name="Apellido">Apellido del cliente.</param>
    /// <param name="Telefono">Telefono del cliente.</param>
    /// <param name="Email">Email del cliente.</param>

    private static int contadorId = 1;
    public Cliente(string Nombre, string Apellido, string Telefono, string Email)
    {
        this.Id = contadorId++;
        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.Telefono = Telefono;
        this.Email = Email;
    }
}