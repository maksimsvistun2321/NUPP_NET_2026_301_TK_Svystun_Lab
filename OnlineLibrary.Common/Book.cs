using System;

namespace OnlineLibrary.Common
{
    //дегегат
    public delegate void ReadingProgressHandler(int pagesRead, double percentage);

    public class Book : Item
    {
        //властивості
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; }
        public DateTime DueDate { get; private set; } = DateTime.Now.AddDays(14);

        //статичне поле
        public static int TotalBooks;

        private int currentPage = 0;

        //подія
        public event ReadingProgressHandler OnProgressChanged;

        //статичний конструктор
        static Book()
        {
            TotalBooks = 0;
        }

        //конструктори
        public Book() : base()
        {
            Author = "Unknown";
            Isbn = "N/A";
            Pages = 0;
            Genre = "Unknown";
            TotalBooks++;
        }

        public Book(string title, string author, int pages, string isbn, string genre) : base()
        {
            Title = title;
            Author = author;
            Pages = pages;
            Isbn = isbn;
            Genre = genre;
            TotalBooks++;
        }

        //метод для оновлення прогресу
        public void UpdateProgress(int currentPage)
        {
            if (currentPage <= Pages)
            {
                currentPage = currentPage;
                double percentage = (double)currentPage / Pages * 100;
                OnProgressChanged?.Invoke(currentPage, percentage);
            }
        }

        //метод для продовження оренди книги
        public void ExtendLease(int days)
        {
            DueDate = DueDate.AddDays(days);
            Console.WriteLine($"\nThe rental period for the book '{Title}' has been extended until {DueDate.ToShortDateString()}.");
        }

        //перевантаження ToString
        public override string ToString()
        {
            return $"\nBook: {Title}, Author={Author}, Pages={Pages}, ISBN={Isbn}, Genre={Genre}, id={Id}";
        }

        //статичний метод
        public static int GetTotalBooks()
        {
            return TotalBooks;
        }
    }
}
