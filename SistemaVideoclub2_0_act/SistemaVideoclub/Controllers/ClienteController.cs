using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVideoclub.Models;
using SistemaVideoclub.Views;
using SistemaVideoclub.Repository;

namespace SistemaVideoclub.Controllers
{
    public class ClienteController
    {
        private Videoclub _videoclub;
        private VideoclubView _view;
        private IRepository<Cliente> _repo;
        private ClienteView _clienteView;

        public ClienteController(Videoclub videoclub, IRepository<Cliente> repo)
        {
            _videoclub = videoclub;
            _view = new VideoclubView();
            _repo = repo;
            _clienteView = new ClienteView();
        }

        public void AgregarCliente()
        {
            _clienteView.MostrarPantallaAgregar(_videoclub.Clientes);

            string nombreCliente = _clienteView.PedirNombreCliente();

            if (nombreCliente == "0")
                return;

            if (string.IsNullOrWhiteSpace(nombreCliente))
            {
                _view.MostrarError("El nombre no puede estar vacío");
                return;
            }

            if (!nombreCliente.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                _view.MostrarError("El nombre solo puede tener letras");
                return;
            }

            if (_videoclub.BuscarCliente(nombreCliente) != null)
            {
                _view.MostrarError("Ese cliente ya existe");
                return;
            }

            string dni;
            while (true)
            {
                dni = _clienteView.PedirDni().Trim();

                if (dni == "0")
                    return;

                if (dni.Length < 7 || dni.Length > 8 || !dni.All(char.IsDigit))
                {
                    _view.MostrarError("El DNI debe tener entre 7 y 8 digitos, sin puntos ni espacios");
                    continue;
                }

                if (_videoclub.Clientes.Any(c => c.Dni == dni))
                {
                    _view.MostrarError("Ya existe un cliente con ese DNI");
                    continue;
                }
                break;
            }

            string telefono = _clienteView.PedirTelefono();

            Cliente nuevoCliente = new Cliente(
                nombreCliente.Trim(),
                dni,
                telefono.Trim()
            );

            _videoclub.AgregarCliente(nuevoCliente);

            _repo.Agregar(nuevoCliente);

            _view.MostrarMensaje("Cliente agregado correctamente");

            _clienteView.MostrarClienteAgregado(nuevoCliente);
        }

        public void EliminarCliente()
        {
            _clienteView.MostrarPantallaEliminar(_videoclub.Clientes);

            if (!int.TryParse(_clienteView.PedirId("eliminar"), out int id))
            {
                _view.MostrarError("ID inválido");
                return;
            }

            Cliente clienteEliminar = _repo.BuscarPorId(id);

            if (clienteEliminar == null)
            {
                _view.MostrarError("Cliente no encontrado");
                return;
            }

            _videoclub.EliminarCliente(
                _videoclub.Clientes.FirstOrDefault(c => c.id == id)
            );

            _repo.Eliminar(id);

            RecargarClientes();

            _view.MostrarMensaje($"Cliente '{clienteEliminar.Nombre}' eliminado correctamente");
        }


        public void ActualizarCliente()
        {
            _clienteView.MostrarPantallaActualizar(_videoclub.Clientes);

            if (!int.TryParse(_clienteView.PedirId("modificar"), out int id))
            {
                _view.MostrarError("ID inválido");
                return;
            }

            Cliente clienteEdit = _repo.BuscarPorId(id);

            if (clienteEdit == null)
            {
                _view.MostrarError("Cliente no encontrado");
                return;
            }

            string nuevoNombre = _clienteView.PedirNuevoNombre();

            if (!string.IsNullOrWhiteSpace(nuevoNombre))
            {
                if (!nuevoNombre.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    _view.MostrarError("El nombre solo puede tener letras");
                    return;
                }

                Cliente repetido = _videoclub.BuscarCliente(nuevoNombre);

                if (repetido != null && repetido.id != clienteEdit.id)
                {
                    _view.MostrarError("Ya existe otro cliente con ese nombre");
                    return;
                }

                clienteEdit.Nombre = nuevoNombre.Trim();
            }

            string nuevoDni = _clienteView.PedirNuevoDni();

            if (!string.IsNullOrWhiteSpace(nuevoDni))
            {
                nuevoDni = nuevoDni.Trim();

                if (nuevoDni.Length < 7 || nuevoDni.Length > 8 || !nuevoDni.All(char.IsDigit))
                {
                    _view.MostrarError("El DNI debe tener entre 7 y 8 digitos, sin puntos ni espacios");
                    return;
                }

                if (_videoclub.Clientes.Any(c => c.id != clienteEdit.id && c.Dni == nuevoDni))
                {
                    _view.MostrarError("Ya existe otro cliente con ese DNI");
                    return;
                }

                clienteEdit.Dni = nuevoDni;
            }

            string nuevoTelefono = _clienteView.PedirNuevoTelefono();

            if (!string.IsNullOrWhiteSpace(nuevoTelefono))
            {
                clienteEdit.Telefono = nuevoTelefono.Trim();
            }

            _repo.Actualizar(clienteEdit);

            RecargarClientes();

            _view.MostrarMensaje("Cliente actualizado correctamente");
        }


        public void VerClientes()
        {
            _clienteView.MostrarPantallaConsulta(_videoclub.Clientes);

            string nombre = _clienteView.PedirNombreCliente();

            Cliente clienteSeleccionado = _videoclub.BuscarCliente(nombre);

            if (clienteSeleccionado == null)
            {
                _view.MostrarError("Cliente no encontrado");
                return;
            }

            _clienteView.MostrarHistorial(clienteSeleccionado);
        }

        private void RecargarClientes()
        {
            _videoclub.Clientes = _repo.LeerTodos();

            foreach (Cliente cliente in _videoclub.Clientes)
            {
                cliente.Prestamos.Clear();

                foreach (Prestamo prestamo in _videoclub.Prestamos)
                {
                    if (prestamo.ClienteNombre == cliente.Nombre)
                    {
                        cliente.AgregarPrestamo(prestamo);
                    }
                }
            }
        }
    }
}
