using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVideoclub.Models;

namespace SistemaVideoclub.Repository
{
    public interface IRepository<T> where T : IEntidad
    {
        List<T> LeerTodos();
        void GuardarTodos(List<T> datos); 
        T? BuscarPorId(int id);
        void Agregar(T item);
        void Actualizar(T item);
        void Eliminar(int id);
    }
}
