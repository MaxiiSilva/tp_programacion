using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using SistemaVideoclub.Models;

namespace SistemaVideoclub.Repository
{
    public class JsonRepository<T> : IRepository<T> where T : IEntidad
    {
        private readonly string _rutaArchivo;
        private void Persistir(List<T> lista)
        {
            var opts = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            File.WriteAllText(_rutaArchivo, 
            JsonSerializer.Serialize(lista, opts));
        }
        public JsonRepository(string ruta)
        {
            _rutaArchivo = ruta;
        }

        public List<T> LeerTodos()
        {
            if (!File.Exists(_rutaArchivo))
                return new();

            var json = File.ReadAllText(_rutaArchivo);

            return JsonSerializer.Deserialize<List<T>>(json) ?? new();
        }

        public void GuardarTodos(List<T> datos)
        {
         
            Persistir(datos);
        }
        public T? BuscarPorId(int id) 
        => LeerTodos().
            FirstOrDefault(x => x.id == id);  

        public void Agregar(T item)
        {

            var lista = LeerTodos();

           
            item.id = lista.Count > 0        
                ? lista.Max(x => x.id) + 1 
                : 1;

            lista.Add(item);

            Persistir(lista);
        }

        
        
        public void Actualizar(T item) 
        {
            var lista = LeerTodos();

           
            var idx = lista.FindIndex(x => x.id == item.id);  
            if (idx >= 0)
            {
                lista[idx] = item;
            }

            Persistir(lista);  
        }
        public void Eliminar(int id) 
        {
            var lista = LeerTodos();
            lista.RemoveAll(e => e.id == id); 

            Persistir(lista);
        }

    }
}
