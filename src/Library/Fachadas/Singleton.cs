namespace Library.Fachadas;
/// <summary>
/// Clase reusable dedicada a aplicar singleton.
/// </summary>
/// <remarks>
/// Clase generica para poder aplicar singleton y no repetir codigo en el proyecto. <see cref="Singleton"/>
/// </remarks>
public class Singleton<T> where T : class, new()
{
    // Despues de hacer el ejercicio en clase llamado PII_Singleton_Start nos dimos cuenta que no estamos utilizando de la mejor manera el patron singleton.
    //Este ejercicio nos llamo la atención, y nos hizo dar cuenta de que perfectamente podriamos estar hacer un singleton mas reusable.
    //Para hacer esto vamos a implementar esta clase singleton que use un tipo generico y ya despues re usarlo en la parte donde sea necesario en el proyecto.
    
    
    // Lazy<T> crea la instancia de T solo cuando se usa por primera vez.
    // También garantiza seguridad en multihilo (no sé si lo vamos a usar al final,
    // pero es bueno tenerlo preparado).
    private static readonly Lazy<T> _instancia = new Lazy<T>(CrearInstancia);


    /// <summary>
    /// Método que crea la instancia del tipo T.
    /// </summary>
    private static T CrearInstancia()
    {
        return new T();
    }

    /// <summary>
    /// Propiedad pública para obtener la instancia única del tipo T.
    /// Siempre devolverá la misma instancia.
    /// </summary>
    public static T Instancia
    {
        get { return _instancia.Value; }
    }

    // Constructor privado para evitar que alguien cree instancias de Singleton<T> con "new".
    // De esta forma, la única manera de obtener una instancia es usando Singleton<T>.Instancia.
    private Singleton() { }
}