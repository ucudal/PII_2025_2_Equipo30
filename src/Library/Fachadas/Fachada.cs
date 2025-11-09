namespace Library.Fachadas;
/// <summary>
/// Fachada principal que se comunica con el bot.
/// </summary>
/// <remarks>
/// Esta es la fachada encargada de comunicarse con messagegateway y acceder a la <see cref="FachadaRegistro"/>
/// </remarks>
public class Fachada
{
    //TODO: Esto se realizará en la 3era entrega donde se implemente el message gateway.
    
    //Empezamos a aplicar lo basico del patrón singleton. En esta versión la vamos a hac
    //Aca tenemos la instancia que va a ser unica den nuestra fachada.

    private static Fachada _instancia;

    private static readonly object _candado;  // Basicamente aca le decimos que no puede cambiar su referencia despues de ser inicializado. De ahi viene su nombre lock en ingles.
    //Sin tener el lock, 2 hilos podrian rear dos instancias y es justamente lo que no queremos.
    // Y ya de paso si para la entrega final, si tendriamos problemas de multihilos, con el lock se solucionaria. Basicamente es para asegurar que funcione aún mas el Singleton.
    private Fachada()
    {
        
    }
    
    public static Fachada ObtenerInstancia()
    {
        if (_instancia == null)
        {
            lock (_candado) // Aca es donde aseguramos que solo un hilo cree la instancia.
            {
                if (_instancia == null)
                {
                    _instancia = new Fachada();
                }
            }
        }
        return _instancia;
    }
    
}