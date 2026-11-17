using System;
using System.Collections.Generic;
using SistemaVideoclub.Model;

namespace SistemaVideoclub.Repository
{
    // Contrato genérico: sirve para Pelicula, Cliente y Prestamo
    public interface IRepository<T> where T : EntidadBase
    {
        void Agregar(T entidad);
        void Modificar(T entidad);
        void Eliminar(int id);
        T ObtenerPorId(int id);
        List<T> ObtenerTodos();
        List<T> Buscar(Func<T, bool> criterio);
    }
}
