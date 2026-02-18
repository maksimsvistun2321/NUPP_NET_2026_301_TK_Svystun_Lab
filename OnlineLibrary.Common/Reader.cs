using System;
using OnlineLibrary.Common;

namespace OnlineLibrary.Common
{
    //делегат
    public delegate void ReaderStatusHandler(string readerStatus);

    public class Reader : IEntity
    {

        //властивості
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string ReaderCurrentStatus { get; private set; } = "Offline";
        private List<string> _favoriteItems = new List<string>();


        //статичне поле
        public static int TotalReaders;
        
        //конструктори
        static Reader()
        {
            TotalReaders = 0;
        }

        public Reader()
        {
            Id = Guid.NewGuid();
            Name = "Reader1";
            Age = 1;
            TotalReaders++;
        }

        public Reader(string name, int age)
        {
            Id = Guid.NewGuid();
            Name = name;
            Age = age;
            TotalReaders++;
        }

        //подія
        public event ReaderStatusHandler OnReaderStatusChanged;

        //метод для зміни статусу
        public void ChangeReaderStatus(string newStatus)
        {
            ReaderCurrentStatus = newStatus;

            if (OnReaderStatusChanged != null)
            {
                OnReaderStatusChanged($"\n'{Name}' status changed. New status: {newStatus}");
            }
        }
        
        //метод для додавання до списку улюбленого
        public void AddToFavorite(Item item)
        {
            _favoriteItems.Add(item.Title);
        }

        //метод для виводу всіх улюблених
        public void PrintAllFavoriteItems()
        {
            Console.Write("\nYour favorite items: ");
            foreach (var item in _favoriteItems)
            {
                Console.Write($"{item} ");
            }
        }

        //метод для видалення із списку улюблених
        public void RemoveFromFavorite(Item item)
        {
            _favoriteItems.Remove(item.Title);
        }

        //перевантаження ToString
        public override string ToString()
        {
            return $"\nReader: {Name} Age: {Age}";
        }

        //статичний метод
        public static int GetTotalReaders()
        {
            return TotalReaders;
        }
    }
}