using System;
using System.Collections.Generic;

namespace OnlineLibrary.Common
{

    //делегат
    public delegate string QuotesFormatter(string author, string title, string magazine);

    public class Article : Item
    {
        //властивості
        public string Author { get; set; }
        public string MagazineName { get; set; }
        public List<string> Keywords { get; set; }

        //статичне поле
        public static int TotalArticles;

        //подія
        public event Action<DateTime> OnPublished;

        //статичний конструктор
        static Article()
        {
            TotalArticles = 0;
        }

        //конструктори
        public Article() : base()
        {
            Author = "Unknown";
            MagazineName = "Unknown";
            Keywords = new List<string>();
            TotalArticles++;
        }

        public Article(string title, string author, string magazineName) : base()
        {
            Title = title;
            Author = author;
            MagazineName = magazineName;
            Keywords = new List<string>();
            TotalArticles++;
        }

        //метод для отримання формлення цитування
        public string GetQuotes(QuotesFormatter formatter)
        {
            if (formatter != null)
            {
                return formatter(Author, Title, MagazineName);
            }
            return $"{Author}, {Title}, {MagazineName}";
        }

        //метод для публікації статті
        public void Publish()
        {
            DateTime publishDate = DateTime.Now;
            Console.WriteLine($"\nArticle '{Title}' published.");
            OnPublished?.Invoke(publishDate);
        }
        //перевантаження методу ToString
        public override string ToString()
        {
            return $"\nArticle: {Title}, MAgazine: {MagazineName}, Author: {Author}";
        }
    }
}
