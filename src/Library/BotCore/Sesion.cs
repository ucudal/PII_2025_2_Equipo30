namespace Library.BotCore
{
    /// <summary>
    /// Representa la información de la sesión.
    /// </summary>
    public class Sesion
    {
        /// <summary>
        /// Representa el tipo de entidad actual (Admin, Usuario, Vendedor).
        /// </summary>
        public object UsuarioActual { get; private set; }
        /// <summary>
        /// Rol del tipo de usuario actual.
        /// </summary>
        public string Rol { get; private set; }
        
        /// <summary>
        /// Indica si existe una sesión activa en el sistema.
        /// </summary>
        /// <value>
        /// Devuelve <c>true</c> si hay un usuario logeado; de lo contrario, <c>false</c>.
        /// </value>
        public bool EstaLogeado 
        {
            get 
            {
                return UsuarioActual != null;
            }
        }

        
        /// <summary>
        /// Inicia una nueva sesión con el usuario y rol especificados.
        /// </summary>
        /// <param name="usuario">Instancia del usuario que inicia sesión.</param>
        /// <param name="rol">Rol del usuario (por ejemplo, "Administrador", "Usuario" o "Vendedor").</param>
        /// <remarks>
        /// Este método establece el usuario actual y marca la sesión como activa.
        /// </remarks>
        public void IniciarSesion(object usuario, string rol)
        {
            UsuarioActual = usuario;
            Rol = rol;
        }

        /// <summary>
        /// Cierra la sesión actual y limpia los datos asociados.
        /// </summary>
        /// <remarks>
        /// Este método borra el usuario y el rol activos, dejando el sistema sin sesión iniciada.
        /// </remarks>
        public void CerrarSesion()
        {
            UsuarioActual = null;
            Rol = null;
        }

        /// <summary>
        /// Devuelve una representación en texto del estado actual de la sesión.
        /// </summary>
        /// <returns>
        /// Un mensaje indicando si hay una sesión activa y el rol del usuario,
        /// o un mensaje indicando que no hay sesión iniciada.
        /// </returns>
        public override string ToString()
        {
            if (EstaLogeado)
            {
                return $"Sesion activa como {Rol}";
            }
            else
            {
                return "No hay sesión activa.";
            }
        }
    }
}