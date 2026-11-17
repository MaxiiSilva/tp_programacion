using System;
using System.Collections.Generic;
using SistemaVideoclub.Model;
using SistemaVideoclub.Repository;

namespace SistemaVideoclub.Controller
{
    public class ClienteController
    {
        private readonly IRepository<Cliente> clientes;
        private readonly IRepository<Prestamo> prestamos;

        public ClienteController(IRepository<Cliente> clientes, IRepository<Prestamo> prestamos)
        {
            this.clientes = clientes;
            this.prestamos = prestamos;
        }

        public void Agregar(string nombre, string dni, string telefono)
        {
            Validar(nombre, dni);
            if (clientes.Buscar(c => c.Dni == dni).Count > 0)
                throw new Exception("Ya existe un cliente con ese DNI.");

            clientes.Agregar(new Cliente { Nombre = nombre, Dni = dni, Telefono = telefono });
        }

        public void Modificar(int id, string nombre, string dni, string telefono)
        {
            Cliente cliente = clientes.ObtenerPorId(id) ?? throw new Exception("El cliente no existe.");
            Validar(nombre, dni);
            if (clientes.Buscar(c => c.Dni == dni && c.Id != id).Count > 0)
                throw new Exception("Ya existe otro cliente con ese DNI.");

            cliente.Nombre = nombre;
            cliente.Dni = dni;
            cliente.Telefono = telefono;
            clientes.Modificar(cliente);
        }

        public void Eliminar(int id)
        {
            if (clientes.ObtenerPorId(id) == null)
                throw new Exception("El cliente no existe.");
            if (prestamos.Buscar(p => p.ClienteId == id && p.Activo).Count > 0)
                throw new Exception("El cliente tiene préstamos activos.");

            clientes.Eliminar(id);
        }

        public List<Cliente> ObtenerTodos() => clientes.ObtenerTodos();

        public List<Cliente> Buscar(string texto)
        {
            texto = (texto ?? "").ToLower();
            return clientes.Buscar(c => c.Nombre.ToLower().Contains(texto) || c.Dni.Contains(texto));
        }

        // Historial de un cliente (equivale al "plantel" del torneo)
        public List<Prestamo> ObtenerHistorial(int clienteId)
        {
            if (clientes.ObtenerPorId(clienteId) == null)
                throw new Exception("El cliente no existe.");

            return prestamos.Buscar(p => p.ClienteId == clienteId);
        }

        private void Validar(string nombre, string dni)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new Exception("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(dni)) throw new Exception("El DNI es obligatorio.");
        }
    }
}
