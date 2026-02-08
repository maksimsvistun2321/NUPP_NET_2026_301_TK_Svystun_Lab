using System;
using OnlineLibrary.Common;

//CRUD сервіси
var bookService = new CrudService<Book>();
var magazineService = new CrudService<Magazine>();
var articleService = new CrudService<Article>();

//стоврення об'єкта book
var book1 = new Book
{
    Title = "Test Book",
    Author = "Test Author",
    Pages = 666,
    Genre = "Programming",
    Isbn = "9780132350884"
};

Console.WriteLine("Testsing book class:");

//подія з базового класу
book1.OnStatusChanged += (msg) => {
    Console.WriteLine($"\n{msg}");
};

//подія з класу Book
book1.OnProgressChanged += (pages, percent) => {
    Console.WriteLine($"\nRead {pages} pages ({percent:0.0}%)");
};

//зміна статусу
book1.ChangeStatus("Reading");

//додавання оцінко
book1.AddRating(5);
book1.AddRating(4);
book1.AddRating(5);

//оновлення прогресу читання
book1.UpdateProgress(100);
book1.UpdateProgress(250);

//подовження терміну оренди
book1.ExtendLease(7);

//Create
bookService.Create(book1);

//Read
var foundBook = bookService.Read(book1.Id);
Console.WriteLine($"\nFound: {foundBook.Title} (Raiting: {foundBook.CalculateRating():0.0})");

//Update
book1.Title = "Test Book 22222";
bookService.Update(book1);

//Save
string filePathBook = "books_data.json";
bookService.Save(filePathBook);

//Load
bookService.Load(filePathBook);

foreach (var b in bookService.ReadAll())
{
    Console.WriteLine(b.ToString());
}

///////////////////////////////////////////////////////////////////////////////////
Console.WriteLine("\n_________________________________________________");
Console.WriteLine("Testing magazine class");

var magazine1 = new Magazine
{
    Title = "Test magazine",
    IssueNumber = 12,
    Publisher = "Test Publisher"
};

//подія з базового класу
magazine1.OnStatusChanged += (msg) => {
    Console.WriteLine($"\n{msg}");
};

//подія з класу Magazine
magazine1.OnPeriodicityChanged += (info) => {
    Console.WriteLine($"\n{info}");
};

//зміна статусу
magazine1.ChangeStatus("In Print");

//використання методу для зміни періодичності. Перший викличе помилку, другий повинене бути успішний
magazine1.UpdatePeriodicity("Daily", (val) => {
    return val == "Weekly" || val == "Monthly";
});

magazine1.UpdatePeriodicity("Weekly", (val) => val == "Weekly" || val == "Monthly");

//додавання оцінко
magazine1.AddRating(4);
magazine1.AddRating(5);
magazine1.AddRating(4);

//Create
magazineService.Create(magazine1);

//Read
var foundMagazine = magazineService.Read(magazine1.Id);
Console.WriteLine($"\nFound: {foundMagazine.Title} (Raiting: {foundMagazine.CalculateRating():0.0})");

//Update
magazine1.IssueNumber = 13;
magazineService.Update(magazine1);

//Save
string filePathMagazine = "magazines_data.json";
magazineService.Save(filePathMagazine);

//Load
magazineService.Load(filePathMagazine);

foreach (var m in magazineService.ReadAll())
{
    Console.WriteLine(m.ToString());
}


///////////////////////////////////////////////////////////////////////////////////
Console.WriteLine("\n_________________________________________________");
Console.WriteLine("Testing article class");

var article1 = new Article 
{
    Title = "Test article",
    Author = "Test Article Author",
    MagazineName = "Test Magazine Name"
};

//подія з базового класу
article1.OnStatusChanged += (msg) => {
    Console.WriteLine($"\n{msg}");
};

//подія 
article1.OnPublished += (date) => {
    Console.WriteLine($"\nArticle published {date:dd.MM.yyyy HH:mm}");
};

//зміна статусу
article1.ChangeStatus("Latest version");

//використання методу для оформлення цитат
Console.WriteLine($"\n{article1.GetQuotes}");

//використання методу класу Article
article1.Publish();

//додавання оцінко
article1.AddRating(4);
article1.AddRating(3);
article1.AddRating(4);

// Create
articleService.Create(article1);

//Read
var foundArticle = articleService.Read(article1.Id);
Console.WriteLine($"\nFound: {foundArticle.Title} (Raiting: {foundArticle.CalculateRating():0.0})");

//Update
article1.Keywords.Add("keyword1");
article1.Keywords.Add("Keyword2");
articleService.Update(article1);

//Save
string filePathArticle = "articles_data.json";
articleService.Save(filePathArticle);

//Load
articleService.Load(filePathArticle);

foreach (var art in articleService.ReadAll())
{
    Console.WriteLine(art.ToString());
}