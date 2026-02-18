using System;

namespace OnlineLibrary.Common
{
    //делегат
    public delegate void ItemStatusHandler(string itemStatus);

    public class Item : IEntity
    {
        //властивості
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Language { get; set; }
        public string CurrentStatus { get; private set; } = "Available";

        //статичне поле
        public static int TotalItems;

        //список для зберігання оцінок
        private List<int> _ratings = new List<int>();

        //статичний конструктор
        static Item()
        {
            TotalItems = 0;
        }

        //конструктори
        public Item()
        {
            Id = Guid.NewGuid();
            Title = "Unknown";
            Description = "Unknown";
            PublicationDate = DateTime.MinValue;
            Language = "English";
            TotalItems++;
        }

        public Item(string title, string description, DateTime publicationDate, string language)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            PublicationDate = publicationDate;
            Language = language;
            TotalItems++;
        }

        //подія
        public event ItemStatusHandler OnItemStatusChanged;

        //метод для додавання оцінок
        public void AddRating(int rating)
        {
            if (rating >= 1 && rating <= 5)
            {
                _ratings.Add(rating);
            }
        }

        //метод для обрахунку рейтингу
        public double CalculateRating()
        {
            if (_ratings.Count == 0) return 0;
            return _ratings.Average();
        }
        
        //метод для зміни статусу
        public void ChangeItemStatus(string newStatus)
        {
            CurrentStatus = newStatus;

            if (OnItemStatusChanged != null)
            {
                OnItemStatusChanged($"\n'{Title}' status changed. New status: {newStatus}");
            }
        }

        //перевантаження методу ToString
        public override string ToString()
        {
            return $"\nItem: {Title}, Rating: {CalculateRating():0.0}, Status: {CurrentStatus}";
        }

        //статичний метод
        public static int GetTotalItems()
        {
            return TotalItems;
        }
    }
}
