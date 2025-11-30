using Library.Clases_principales;

namespace Library.Fachadas
{
    public class FachadaRegistro
    {
        //Tener en cuenta que para mantener que se inicie una sola instancia debemos usar la clase singleton.
        /// <summary>
        /// Usamos singleton para llamar a la fachada usando nuestra clase. Ejemplo: var fachada = Singleton<FachadaRegistro>.Instancia;
        /// </summary>
        /// <summary>
        /// Proporciona una interfaz simplificada para la gestión de usuarios y clientes
        /// dentro del sistema de registro.
        /// </summary>
        /// <remarks>
        /// Esta fachada encapsula la interacción con la clase <see cref="Administrador"/>,
        /// y ofrece métodos de alto nivel para crear, modificar, listar y eliminar usuarios y clientes.
        /// </remarks>
        private Administrador _admin;

        
        /// <summary>
        /// Lista de vendedores, almacena <see cref="Vendedor"/>
        /// </summary>
        public List<Vendedor> Vendedores = new List<Vendedor>();

        /// <summary>
        /// Crea un nuevo vendedor
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="clave"></param>
        /// <returns>True si se pudo crear al vendedor, False si no.</returns>
        public bool CrearVendedor(string nombre, string clave)
        {
            //verifica que no haya otro vendedor con ese nombre.
            if (Vendedores.Any(v => v.Nombre == nombre))
            {
                return false;
            }
            Vendedor vendedor = new Vendedor(nombre, clave);
            Vendedores.Add(vendedor);
            return true;
        }
        
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FachadaRegistro"/>
        /// con un administrador predeterminado.
        /// </summary>
        public FachadaRegistro()
        {
            _admin = new Administrador("admin", "admin");
        }

        /// <summary>
        /// Metodo para loguear a un administrador.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="clave"></param>
        /// <returns>Instancia de administrador si se pudo logear o Null si no.</returns>
        public Administrador LoginAdministrador(string nombre, string clave)
        {
            if (_admin.Nombre == nombre && _admin.Clave == clave)
                return _admin;
            return null;
        }

        /// <summary>
        /// Metodo para loguear a un usuario.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="clave"></param>
        /// <returns>Instancia usuario si se pudo logear o Null si no.</returns>
        public Usuario LoginUsuario(string nombre, string clave)
        {
            return _admin.Usuarios.FirstOrDefault(u => u.Nombre == nombre && u.Clave == clave); //LINQ para buscar usuario
        }

        /// <summary>
        /// Metodo para loguear a un vendedor.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="clave"></param>
        /// <returns>Instancia de vendedor si se pudo logear o Null si no.</returns>
        public Vendedor LoginVendedor(string nombre, string clave)
        {
            return Vendedores.FirstOrDefault(v => v.Nombre == nombre && v.Clave == clave); //LINQ para buscar vendedor
        }
        
        // --- Historias de usuario ---

        /// <summary>
        /// Crea un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="nombre">Nombre del nuevo usuario.</param>
        /// <param name="clave">Clave o contraseña del usuario.</param>
        /// <returns>
        /// El objeto <see cref="Usuario"/> creado, o <c>null</c> si no se pudo crear.
        /// </returns>
        public Usuario CrearUsuario(string nombre, string clave)
        {
            return _admin.CrearUsuario(nombre, clave);
        }

        /// <summary>
        /// Elimina un usuario existente del sistema.
        /// </summary>
        /// <param name="nombre">Nombre del usuario a eliminar.</param>
        /// <returns>
        /// <c>true</c> si el usuario fue eliminado correctamente; <c>false</c> en caso contrario.
        /// </returns>
        public bool EliminarUsuario(string nombre)
        {
            return _admin.EliminarUsuario(nombre);
        }

        /// <summary>
        /// Crea un nuevo cliente y lo asocia al usuario especificado.
        /// </summary>
        /// <param name="usuario">Instancia del <see cref="Usuario"/> al que se asignará el cliente.</param>
        /// <param name="nombre">Nombre del cliente.</param>
        /// <param name="apellido">Apellido del cliente.</param>
        /// <param name="telefono">Teléfono del cliente.</param>
        /// <param name="email">Correo electrónico del cliente.</param>
        /// <returns>El objeto <see cref="Cliente"/> creado.</returns>
        /// <remarks>
        /// Este metodo crea el <c>RegistroCliente</c> con el <c>Cliente</c> dentro y ya lo almacena en el <c>Usuario</c>.
        /// </remarks>
        public bool CrearCliente(Usuario usuario, string nombre, string apellido, string telefono, string email)
        {
            if (usuario == null) return false;
            
            var cliente = new Cliente(nombre, apellido, telefono, email);
            var registro = new RegistroCliente(cliente);
            usuario.Clientes.Add(registro);
            return true;
        }

        /// <summary>
        /// Modifica los datos de un cliente existente.
        /// </summary>
        /// <param name="usuario">Usuario propietario del cliente.</param>
        /// <param name="id">Identificador del cliente a modificar.</param>
        /// <param name="nuevoNombre">Nuevo nombre del cliente (opcional).</param>
        /// <param name="nuevoEmail">Nuevo correo electrónico del cliente (opcional).</param>
        /// <returns>
        /// <c>true</c> si la modificación se realizó correctamente; <c>false</c> si no se encontró el cliente.
        /// </returns>
        public bool ModificarCliente(
            Usuario usuario,
            int id,
            string nuevoNombre = null,
            string nuevoApellido = null,
            string nuevoTelefono = null,
            string nuevoEmail = null,
            string nuevoGenero = null,
            string nuevoCumple = null,
            string nuevaEtiqueta = null)
        {
            if (usuario == null) return false;

            return usuario.ModificarCliente(
                id,
                nuevoNombre,
                nuevoApellido,
                nuevoTelefono,
                nuevoEmail,
                nuevoGenero,
                nuevoCumple,
                nuevaEtiqueta
            );
        }


        /// <summary>
        /// Obtiene una lista de todos los clientes asociados al usuario.
        /// </summary>
        /// <param name="usuario">Usuario del cual se listarán los clientes.</param>
        /// <returns>Una lista de objetos <see cref="Cliente"/>.</returns>
        public List<Cliente> ListarClientes(Usuario usuario)
        {
            return usuario.ListarClientes();
        }

        /// <summary>
        /// Elimina un cliente asociado a un usuario.
        /// </summary>
        /// <param name="usuario">Usuario propietario del cliente.</param>
        /// <param name="cliente">Cliente a eliminar.</param>
        /// <returns>
        /// <c>true</c> si el cliente fue eliminado correctamente; <c>false</c> en caso contrario.
        /// </returns>
        public bool EliminarCliente(Usuario usuario, Cliente cliente)
        {
            return usuario.EliminarCliente(cliente);
        }
        
        /// <summary>
        /// Elimina a un usuario por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True si se eliminó, False si no.</returns>
        public bool EliminarUsuarioPorId(int id)
        {
            var usuario = _admin.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null) return false;

            _admin.Usuarios.Remove(usuario);
            return true;
        }

        /// <summary>
        /// Obtiene un total de ventas en un periodo dado.
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="clienteId"></param>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <returns>int total de ventas.</returns>
        public int ObtenerTotalVentasPorPeriodo(Usuario usuario, int clienteId, DateTime desde, DateTime hasta)
        {
            if (usuario == null) return 0;
            return usuario.TotalVentasPorPeriodo(clienteId, desde, hasta);
        }

        /// <summary>
        /// Busca a un cliente por nombre, apellido, telefono o email.
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="telefono"></param>
        /// <param name="email"></param>
        /// <returns>Lista de clientes que coinciden.</returns>
        public List<Cliente> BuscarClientes(Usuario usuario, string nombre = null, string apellido = null, string telefono = null, string email = null)
        {
            if (usuario == null) return new List<Cliente>();
            return usuario.BuscarClientes(nombre, apellido, telefono, email);
        }
        
        /// <summary>
        /// Suspende a un usuario.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True si se suspendió, False si no.</returns>
        public bool SuspenderUsuario(int id)
        {
            var usuario = _admin.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null) return false;

            usuario.Suspendido = true; // Necesitas agregar esta propiedad en Usuario
            return true;
        }

        /// <summary>
        /// Obtiene un panel resumen de un usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Panel resumen.</returns>
        public PanelResumen ObtenerPanelResumen(Usuario usuario)
        {
            if (usuario == null) return null;
            return usuario.ObtenerPanelResumen();
        }

    }
}