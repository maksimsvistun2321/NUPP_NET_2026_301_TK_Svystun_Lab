using System;

namespace OnlineLibrary.Common
{
    //делегат
    public delegate bool PeriodicityValidator(string newPeriod);

    public class Magazine : Item
    {
        //властивості
        public int IssueNumber { get; set; }
        public string Publisher { get; set; }
        public string Periodicity { get; set; }

        //статичне поле
        public static int TotalMagazines;

        //подія
        public event Action<string> OnPeriodicityChanged;

        //статичний констурктор
        static Magazine()
        {
            TotalMagazines = 0;
        }
         
        //конструктори
        public Magazine() : base()
        {
            IssueNumber = 1;
            Publisher = "Unknown";
            Periodicity = "Monthly";
            TotalMagazines++;
        }

        public Magazine(string title, int issueNumber, string publisher) : base()
        {
            Title = title;
            IssueNumber = issueNumber;
            Publisher = publisher;
            Periodicity = "Monthly";
            TotalMagazines++;
        }

        //метод для зміни періодичсноті
        public void UpdatePeriodicity(string newPeriod, PeriodicityValidator validator)
        {
            if (validator != null && validator(newPeriod))
            {
                string oldPeriod = Periodicity;
                Periodicity = newPeriod;
                OnPeriodicityChanged?.Invoke($"\nMagazine '{Title}' changed schedule: was '{oldPeriod}', became '{newPeriod}'");
            }
            else
            {
                Console.WriteLine($"\nChanged error!!!");
            }
        }
        //перевантаження метода ToString
        public override string ToString()
        {
            return $"\nMagazine: {Title}, Periodicity: {Periodicity}";
        }
    }
}
