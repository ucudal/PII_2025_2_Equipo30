namespace Library.Clases_principales;

/// <summary>
/// Administra y almacena una lista de <see cref="Usuario"/>.
/// </summary>
/// <remarks>
/// Esta clase puede crear y eliminar usuarios.
/// </remarks>
public class Administrador
{
    public string Nombre { get; set; }
    public string Clave { get; set; }
    
    /// <summary>
    /// Lista de usuarios que administra
    /// </summary>
    /// <value>
    /// Contiene <see cref="Usuario"/>
    /// </value>
    /// <remarks>La lista no puede contener dos usuarios con el mismo nombre.</remarks>
    public List<Usuario> Usuarios { get; set; } = new List<Usuario>(); //Lista de usuarios administra
    
    
    /// <summary>
    /// Crea un usuario y lo agrega a la lista de usuarios.
    /// </summary>
    /// <param name="nombre">Nombre del usuario a crear.</param>
    /// <param name="clave">Clave de acceso del usuario.</param>
    /// <returns>
    /// <c>Usuario</c> si el usuario fue creado correctamente.
    /// <c>Null</c> si ya existe un <see cref="Usuario"/> con el mismo nombre.
    /// </returns>
    /// <remarks>
    /// El método verifica primero si ya existe un usuario con el nombre indicado.
    /// Si el nombre ya está en uso, no se crea un nuevo usuario y se devuelve <c>Null</c>.
    /// </remarks>
    public Usuario CrearUsuario(string nombre, string clave)
    {
        // Verifica si ya existe un usuario con ese nombre
        if (Usuarios.Any(u => u.Nombre == nombre))
        {
            return null; // No se crea
        }

        var usuario = new Usuario { Nombre = nombre, Clave = clave };
        Usuarios.Add(usuario);
        return usuario;
    }

    /// <summary>
    /// Elimina el usuario de la lista de usuarios.
    /// </summary>
    /// <param name="nombre">Nombre del usuario a eliminar.</param>
    /// <returns>
    /// <c>true</c>Si se encontró un usuario con el mismo nombre y se eliminó.
    /// <c>false</c>Si no se encontró a un usuario con ese nombre.
    /// </returns>
    public bool EliminarUsuario(string nombre)
    {
        var usuario = Usuarios.FirstOrDefault(u => u.Nombre == nombre); //usa LINQ para buscar y guardar al primer usuario que coincida con el nombre en usuario. Esto debido a que nos gustaria poder implementar una base de datos basica para la entrega final.
        if (usuario == null)
            return false; // No se encontró el usuario

        Usuarios.Remove(usuario);
        return true; // Usuario eliminado exitosamente
    }

    /// <summary>
    /// Constructor de la clase.
    /// </summary>
    /// <param name="nombre">Nombre del administrador.</param>
    /// <param name="clave">Clave del administrador.</param>
    public Administrador(string nombre, string clave)
    {
        this.Nombre = nombre;
        this.Clave = clave;
    }
}