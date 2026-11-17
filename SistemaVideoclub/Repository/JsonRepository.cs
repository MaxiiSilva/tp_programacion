using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using SistemaVideoclub.Model;

namespace SistemaVideoclub.Repository
{
    // Implementación genérica: guarda una lista de T en un archivo JSON
    public class JsonRepository<T> : IRepository<T> where T : EntidadBase
    {
        private readonly string ruta;
        private List<T> items;

        private static readonly JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public JsonRepository(string nombreArchivo)
        {
            Directory.CreateDirectory("Data");
            ruta = Path.Combine("Data", nombreArchivo);
            items = Cargar();
        }

        public void Agregar(T entidad)
        {
            // Id autoincremental: el mayor existente + 1
            entidad.Id = items.Count == 0 ? 1 : items.Max(x => x.Id) + 1;
            items.Add(entidad);
            Guardar();
        }

        public void Modificar(T entidad)
        {
            int indice = items.FindIndex(x => x.Id == entidad.Id);
            if (indice == -1)
                throw new Exception("No se encontró el registro a modificar.");

            items[indice] = entidad;
            Guardar();
        }

        public void Eliminar(int id)
        {
            T entidad = ObtenerPorId(id);
            if (entidad == null)
                throw new Exception("No se encontró el registro a eliminar.");

            items.Remove(entidad);
            Guardar();
        }

        public T ObtenerPorId(int id)
        {
            return items.FirstOrDefault(x => x.Id == id);
        }

        public List<T> ObtenerTodos()
        {
            return items.ToList(); // copia, para no exponer la lista interna
        }

        public List<T> Buscar(Func<T, bool> criterio)
        {
            return items.Where(criterio).ToList();
        }

        // ----- Persistencia -----
        private List<T> Cargar()
        {
            if (!File.Exists(ruta))
                return new List<T>();

            string json = File.ReadAllText(ruta);
            if (string.IsNullOrWhiteSpace(json))
                return new List<T>();

            return JsonSerializer.Deserialize<List<T>>(json, opciones) ?? new List<T>();
        }

        public void Guardar()
        {
            string json = JsonSerializer.Serialize(items, opciones);
            File.WriteAllText(ruta, json);
        }
    }
}
