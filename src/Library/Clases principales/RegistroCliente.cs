using System.Globalization;
using Library.Clases_tipos;

namespace Library.Clases_principales;

/// <summary>
/// Almacena todas las interacciones con un <see cref="Cliente"/> en especifico.
/// </summary>
/// <remarks>
/// Esta clase también puede agregar información especifica al <see cref="Cliente"/> como Nacimiento, Genero y Precios.
/// </remarks>
public class RegistroCliente
{
    public Cliente Cliente { get; set; }
    public Mensajes Mensajes { get; set; }
    public Emails Emails { get; set; }
    public Llamadas Llamadas { get; set; }
    public Reunion Reunion { get; set; }
    public Ventas Ventas { get; set; }
    public Precio Precio { get; set; }

    /// <summary>
    /// Modifica el atributo fechaNacimiento de <see cref="Cliente"/>.
    /// </summary>
    /// <param name="FechaNacimiento">Fecha de nacimiento a registrar.</param>
    /// <returns>
    /// <c>true</c> si el parametro fechaNacimiento no está vacío y se pudo registrar correctamente.
    /// <c>false</c> si el parametro fechaNacimiento está vacío o no se pudo registrar correctamente.
    /// </returns>
    public bool RegistrarNacimiento(string FechaNacimiento)
    {
        if (string.IsNullOrEmpty(FechaNacimiento))
            return false;

        Cliente.FechaNacimiento = FechaNacimiento;
        return true;
    }

    /// <summary>
    /// Devuelve un DateTime con la fecha de la ultima interacción.
    /// </summary>
    /// <returns>DateTime</returns>
    public DateTime UltimaInteraccion()
{
    DateTime ultima = DateTime.MinValue;
    DateTime fechaActual;
    
    if (this.Mensajes.mensajesEnviados != null)
    {
        foreach (var m in this.Mensajes.mensajesEnviados)
        {
            if (DateTime.TryParseExact(m.Fecha, "dd/MM/yy", null,
                DateTimeStyles.None, out fechaActual))
            {
                if (fechaActual > ultima)
                {
                    ultima = fechaActual;
                }
            }
        }
    }
    
    if (this.Mensajes.mensajesRecibidos != null)
    {
        foreach (var m in this.Mensajes.mensajesRecibidos)
        {
            if (DateTime.TryParseExact(m.Fecha, "dd/MM/yy", null,
                DateTimeStyles.None, out fechaActual))
            {
                if (fechaActual > ultima)
                {
                    ultima = fechaActual;
                }
            }
        }
    }
    
    if (this.Emails.Enviados != null)
    {
        foreach (var e in this.Emails.Enviados)
        {
            if (DateTime.TryParseExact(e.Fecha, "dd/MM/yy", null,
                DateTimeStyles.None, out fechaActual))
            {
                if (fechaActual > ultima)
                {
                    ultima = fechaActual;
                }
            }
        }
    }
    
    if (this.Emails.Recibidos != null)
    {
        foreach (var e in this.Emails.Recibidos)
        {
            if (DateTime.TryParseExact(e.Fecha, "dd/MM/yy", null,
                DateTimeStyles.None, out fechaActual))
            {
                if (fechaActual > ultima)
                {
                    ultima = fechaActual;
                }
            }
        }
    }
    
    if (this.Llamadas.Enviados != null)
    {
        foreach (var l in this.Llamadas.Enviados)
        {
            if (DateTime.TryParseExact(l.Fecha, "dd/MM/yy", null,
                DateTimeStyles.None, out fechaActual))
            {
                if (fechaActual > ultima)
                {
                    ultima = fechaActual;
                }
            }
        }
    }
    
    if (this.Llamadas.Recibidos != null)
    {
        foreach (var l in this.Llamadas.Recibidos)
        {
            if (DateTime.TryParseExact(l.Fecha, "dd/MM/yy", null,
                DateTimeStyles.None, out fechaActual))
            {
                if (fechaActual > ultima)
                {
                    ultima = fechaActual;
                }
            }
        }
    }
    
    if (!string.IsNullOrEmpty(this.Reunion.Fecha))
    {
        if (DateTime.TryParseExact(this.Reunion.Fecha, "dd/MM/yy", null,
            DateTimeStyles.None, out fechaActual))
        {
            if (fechaActual > ultima)
            {
                ultima = fechaActual;
            }
        }
    }

    return ultima;
}


/// <summary>
    /// Modifica el atributo Genero de <see cref="Cliente"/>.
    /// </summary>
    /// <param name="genero">Genero a registrar.</param>
    /// <returns>
    /// <c>true</c> si el parametro genero no está vacío y se pudo registrar correctamente.
    /// <c>false</c> si el parametro genero está vacío o no se pudo registrar correctmanete.
    /// </returns>
    public bool RegistrarGenero(string genero)
    {
        if (string.IsNullOrEmpty(genero))
            return false;

        Cliente.Genero = genero;
        return true;
    }
    /// <summary>
    /// Modifica el atributo <see cref="Precio"/> de <see cref="Cliente"/>.
    /// </summary>
    /// <param name="precio"><see cref="Precio"/> a registrar.</param>
    /// <returns>
    /// <c>true</c> si el parametro precio no es null y se pudo registrar correctamente.
    /// <c>false</c> si el parametro precio es null o no se pudo registrar correctmanete.
    /// </returns>
    public bool RegistrarPrecio(Precio precio)
    {
        if (precio == null)
            return false;

        this.Precio = precio;
        return true;
    }
    
    /// <summary>
    /// Constructor de <see cref="RegistroCliente"/>
    /// </summary>
    /// <param name="cliente">Cliente a mantener registro.</param>
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