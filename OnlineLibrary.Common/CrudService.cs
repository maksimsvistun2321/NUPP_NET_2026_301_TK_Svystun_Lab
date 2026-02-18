using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace OnlineLibrary.Common
{
    public class CrudService<T> : ICrudService<T> where T : IEntity
    {
        private readonly Dictionary<Guid, T> db;

        public CrudService()
        {
            db = new Dictionary<Guid, T>();
        }

        //CREATE
        public void Create(T element)
        {
            if (element.Id == Guid.Empty)
            {
                element.Id = Guid.NewGuid();
            }

            db[element.Id] = element;
            Console.WriteLine($"\nElement created: Value={element}");
        }

        //READ
        public T Read(Guid id)
        {
            db.TryGetValue(id, out var element);
            return element;
        }

        //READ ALL
        public IEnumerable<T> ReadAll()
        {
            return db.Values;
        }

        //UPDATE
        public void Update(T element)
        {
            if (!db.ContainsKey(element.Id))
            {
                throw new KeyNotFoundException("\nElement not found");
            }

            db[element.Id] = element;
        }

        //DELETE
        public void Remove(T element)
        {
            db.Remove(element.Id);
        }

        //SAVE
        public void Save(string FilePath)
        {
            try
            {
                string json = JsonSerializer.Serialize(db.Values);
                File.WriteAllText(FilePath, json);
                Console.WriteLine($"\nData saved to {FilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }

        //LOAD
        public void Load(string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("\nFile doesn`t exist. Try againg.");
                return;
            }

            try
            {
                string json = File.ReadAllText(FilePath);
                var elements = JsonSerializer.Deserialize<List<T>>(json);

                db.Clear();
                if (elements != null)
                {
                    foreach (var item in elements)
                    {
                        db[item.Id] = item;
                    }
                }
                Console.WriteLine($"\nLoaded {db.Count} elements.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }
    }
}
