namespace Library.Clases;

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido {get; set;}
    public string Telefono {get; set;}
    public string Email { get; set; }
    public string Genero { get; set; }
    public string fechaNacimiento { get; set; }
    public string Etiqueta {get; set;}

    public void Contactar()
    {
        //TODO
    }

    public Cliente(string Nombre, string Apellido, string Telefono, string Email)
    {
        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.Telefono = Telefono;
        this.Email = Email;
        //TODO ¿ID lo define el usuario que registra al cliente o se define automaticamente en base al registro?
    }
}