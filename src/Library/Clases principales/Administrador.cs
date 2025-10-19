namespace Library.Clases;

public class Administrador
{
    public string Nombre { get; set; }
    public string Clave { get; set; }

    public void CrearUsuario()
    {
        //TODO 
    }

    public void EliminarUsuario()
    {
        //TODO
    }

    public Administrador(string nombre, string clave)
    {
        this.Nombre = nombre;
        this.Clave = clave;
    }
}