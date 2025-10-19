using Library.Clases_tipos;

namespace Library.Clases_principales;

public class RegistroCliente
{
    public Cliente Cliente { get; set; }
    public Mensajes Mensajes { get; set; }
    public Emails Emails { get; set; }
    public Llamadas Llamadas { get; set; }
    public Reunion Reunion { get; set; }
    public Ventas Ventas { get; set; }
    public Precio Precio { get; set; }
    
    public bool RegistrarNacimiento(string fechaNacimiento)
    {
        if (string.IsNullOrEmpty(fechaNacimiento))
            return false;

        Cliente.fechaNacimiento = fechaNacimiento;
        return true;
    }
    
    public bool RegistrarGenero(string genero)
    {
        if (string.IsNullOrEmpty(genero))
            return false;

        Cliente.Genero = genero;
        return true;
    }
    
    public bool RegistrarPrecio(Precio precio)
    {
        if (precio == null)
            return false;

        this.Precio = precio;
        return true;
    }
    
    public RegistroCliente(Cliente cliente)
    {
        this.Cliente = cliente;
        this.Mensajes = new Mensajes();
        this.Emails = new Emails();
        this.Llamadas = new Llamadas();
        this.Reunion = new Reunion();
        this.Ventas = new Ventas();
        this.Precio = new Precio();
    }

}