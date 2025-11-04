using Library.Clases_principales;

namespace Library.Fachadas
{
    public class FachadaRegistro
    {
        private Administrador _admin;

        public FachadaRegistro()
        {
            _admin = new Administrador("admin", "admin");
        }

        // historias de usuario

        public Usuario CrearUsuario(string nombre, string clave)
        {
            return _admin.CrearUsuario(nombre, clave);
        }

        public bool EliminarUsuario(string nombre)
        {
            return _admin.EliminarUsuario(nombre);
        }

        public Cliente CrearCliente(Usuario usuario, string nombre, string apellido, string telefono, string email)
        {
            var cliente = new Cliente(nombre, apellido, telefono, email);
            var registro = new RegistroCliente(cliente);
            usuario.Clientes.Add(registro);
            return cliente;
        }

        public bool ModificarCliente(Usuario usuario, int id, string nuevoNombre = null, string nuevoEmail = null)
        {
            return usuario.ModificarCliente(id, nuevoNombre, null, null, nuevoEmail);
        }

        public List<Cliente> ListarClientes(Usuario usuario)
        {
            return usuario.ListarClientes();
        }

        public bool EliminarCliente(Usuario usuario, Cliente cliente)
        {
            return usuario.EliminarCliente(cliente);
        }
        
        //TODO Implementar sistema de registro de mensajes o información de cliente
    }
}