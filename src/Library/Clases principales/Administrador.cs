namespace Library.Clases_principales;

public class Administrador
{
    public string Nombre { get; set; }
    public string Clave { get; set; }
    public List<Usuario> Usuarios { get; set; } = new List<Usuario>(); //Lista de usuarios administra
    
    public Usuario CrearUsuario(string nombre, string clave) //TODO O cambiar la forma de eliminar usuario por la clave o agregar caso en que nombre de usuario coincida con otro
    {
        var usuario = new Usuario { Nombre = nombre, Clave = clave };
        Usuarios.Add(usuario);
        return usuario; // Devuelve el usuario creado 
    }

    public bool EliminarUsuario(string nombre)
    {
        var usuario = Usuarios.FirstOrDefault(u => u.Nombre == nombre); //usa LINQ para buscar y guardar al primer usuario que coincida con el nombre en usuario
        if (usuario == null)
            return false; // No se encontró el usuario

        Usuarios.Remove(usuario);
        return true; // Usuario eliminado exitosamente
    }

    public Administrador(string nombre, string clave)
    {
        this.Nombre = nombre;
        this.Clave = clave;
    }
}