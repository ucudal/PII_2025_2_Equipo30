namespace Library.Fachadas;
/// <summary>
/// Fachada principal que se comunica con el bot.
/// </summary>
/// <remarks>
/// Esta es la fachada encargada de comunicarse con messagegateway y acceder a la <see cref="FachadaRegistro"/>
/// </remarks>

public class Fachada
{
    /// <summary>
    /// Constructor privado para evitar que otras clases puedan crear instancias
    /// de Fachada usando "new". 
    /// 
    /// Esto es necesario para que la única manera de obtener una instancia 
    /// sea a través de:  Singleton<Fachada>.Instancia
    /// </summary>
    private Fachada()
    {
        
    }

    // Ejemplo: Para obtener la instancia única de esta clase, usar:
    //     var fachada = Singleton<Fachada>.Instancia;
    // Esto devuelve siempre la misma instancia de Fachada, que es manejada por el singleton genérico.
}
