using Library.Clases_tipos;

namespace Library.Clases;

public class RegistroCliente
{
    public Cliente Cliente { get; set; }
    public Mensajes Mensajes { get; set; }
    public Emails Emails { get; set; }
    public Llamadas Llamadas { get; set; }
    public Reunion Reunion { get; set; }
    public Ventas Ventas { get; set; }
    public Precio Precio { get; set; }

    public void RegistrarNacimiento()
    {
        //TODO
    }

    public void RegistrarGenero()
    {
        //TODO
    }

    public void RegistrarPrecio()
    {
        //TODO
    }
    
    //TODO ¿Constructor?
    //La fachada (pendiente) almacena lista de RegistroCliente.
}