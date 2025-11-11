// See https://aka.ms/new-console-template for more information




using System.Diagnostics;
using System.Xml.Linq;

bool condition = true;
Library library = new Library();

while (condition)
{
    printMainMenu();

    int var = 0;
    bool result = false;
    while (!result)
    {
        Console.WriteLine("Введите число выбранного пункта");
        result = int.TryParse(Console.ReadLine(), out var);
    }

    switch (var)
    {
        case 0: condition = false; break;
        case 1: library.addBook(); break;
        case 2: library.deleteBook(); break;
        case 3: library.findingBook(); break;
        case 4: library.sortingBooks(); break;
        case 5: library.cheapExpensiveBooks(); break;
        case 6: library.groupAuthors(); break;
            case 7: library.printLibraryBooks(); break;
        default: Console.WriteLine("Некорректный ввод. Введите число."); break;
    }
}



void printMainMenu()
{
    Console.WriteLine("УЧЁТ КНИГ В БИБЛИОТЕКЕ");
    Console.WriteLine("1. ДОБАВИТЬ книгу");
    Console.WriteLine("2. УДАЛИТЬ книгу по идентификатору");
    Console.WriteLine("3. НАЙТИ книгу");
    Console.WriteLine("4. ОТСОРТИРОВАТЬ книги");
    Console.WriteLine("5. НАЙТИ самую дорогую и самую дешевую книгу");
    Console.WriteLine("6. СГРУППИРОВАТЬ книги по авторам и вывести количество книг каждого автора");
    Console.WriteLine("7. ВЫВЕСТИ ВСЕ доступные книги");
    Console.WriteLine("0. Закончить работу");
}
enum Genre
{
    Classic,
    Detective,
    SciFi,
    Fantasy
}
class Book
{
    public int ID;
    public string name;
    public string author;
    public Genre genre;
    public int year;
    public float price;
    public Book(int id)
    {
        ID = id;
    }

    public Book(int id, string name, string author, Genre genre, int year, float price)
    {
        ID = id;
        this.name = name;
        this.author = author;
        this.genre = genre;
        this.year = year;
        this.price = price;
    }

    public void printInfo()
    {
        Console.WriteLine("ID: {0, 3} Название: {1, 25} Автор: {2, 20}  Жанр: {3, 10} Год: {4, 5} Цена: {5, 9}", ID.ToString(), name, author, genre.ToString(), year.ToString(), price.ToString());
    }
}
class Library
{
    private List<Book> books = new List<Book>();
    private int nextID = 1;

    public Library()
    {
        // Добавление тестовых книг
        books.Add(new Book(nextID++, "Война и Мир", "Лев Толстой", Genre.Classic, 1869, 690));
        books.Add(new Book(nextID++, "Преступление и наказание", "Федор Достоевский", Genre.Classic, 1866, 1200));
        books.Add(new Book(nextID++, "Мастер и Маргарита", "Михаил Булгаков", Genre.Fantasy, 1967, 700));
        books.Add(new Book(nextID++, "Отцы и дети", "Иван Тургенев", Genre.Classic, 1862, 500));
        books.Add(new Book(nextID++, "Евгений Онегин", "Александр Пушкин", Genre.Classic, 1833, 250));
    }

    public void printLibraryBooks()
    {

        foreach (Book book in books)
        {
            book.printInfo();
        }
        ;

    }



public void addBook()
    {
        // запросить все параметры у пользователя,
        // идентификатор назначается автоматически

        bool flag = false;

        while (!flag)
        {
            Console.WriteLine("Введите название книги:");
            string name = Console.ReadLine();
            Genre genre = new Genre();

            if(string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Пустое название книги. Попробуйте ещё раз");
                break;
            }

            Console.WriteLine("Введите автора:");
            string author = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Нет автора. Попробуйте ещё раз");
                break;
            }

            Console.WriteLine("Выберите жанр: 0 - Classic, 1 - Detective, 2 - SciFi, 3 - Fantasy");

            bool resG = false;
            int genreChoice;
            while (!resG)
            {
                Console.WriteLine("Введите число выбранного пункта");
                resG = int.TryParse(Console.ReadLine(), out genreChoice);

                if (!resG || (genreChoice != 0 && genreChoice != 1 && genreChoice != 2 && genreChoice != 3))
                {
                    Console.WriteLine("Неверный ввод. Такого жанра нет. Попробуйте ещё раз");
                    resG = false;
                }
                else
                {
                     genre = (Genre)genreChoice;
                    resG = true;
                }

            }

            bool resY = false;
            int year = 0;

            while (!resY)
            {
                Console.WriteLine("Введите год издания:");
                resY = int.TryParse(Console.ReadLine(),out year);

                if (!resY)
                {
                    Console.WriteLine("Неверный ввод года. Попробуйте ещё раз");
                }
                else if (year < 1 || year > DateTime.Now.Year)
                {
                    Console.WriteLine("Неверный ввод года. Попробуйте ещё раз");
                    resY = false;
                }

            }

            bool resP = false;
            float price = 0;

            while (!resP)
            {
                Console.WriteLine("Введите цену:");
                resP = float.TryParse(Console.ReadLine(), out price);

                if (!resP)
                {
                    Console.WriteLine("Неверный ввод цены. Попробуйте ещё раз");
                } else if (price <= 0)
                {
                    Console.WriteLine("Неверный ввод цены. Попробуйте ещё раз");
                    resP = false;
                }

            }

            if (resG == true && resY  == true && resP == true)
            {
                Book newBook = new Book(nextID++)
                {
                    name = name,
                    author = author,
                    genre = genre,
                    year = year,
                    price = price
                };

                books.Add(newBook);
                Console.WriteLine("Книга добавлена успешно!");
                flag = true;
            } else
            {
                Console.WriteLine("Что-то пошло не так. Попробуйте ещё раз");
                flag = true;
            }

        }

    }

    public void autoaddbook(string name, string author, Genre genre, int year, float price)
    {
        Book newBook = new Book(nextID++)
        {
            name = name,
            author = author,
            genre = genre,
            year = year,
            price = price
        };

        books.Add(newBook);
    }

    public void deleteBook()
    {
        // Удалить книгу по идентификатору

        Console.WriteLine("Введите ID книги для удаления:");
        int id = -1;

        bool res = false;
        while (!res)
        {
            Console.WriteLine("Введите ID книги");
            res = int.TryParse(Console.ReadLine(), out id);
        }


        Book booktodelete = books.Find(b => b.ID == id);

        if (id != -1)
        {
            books.Remove(booktodelete);
            Console.WriteLine("Книга удалена");
        }
        else
        {
            Console.WriteLine("Книга с таким ID не найдена");
        }

    }

    void printFindingMenu()
    {
        Console.WriteLine("ПОИСК КНИГИ ПО");
        Console.WriteLine("1 -- Названию");
        Console.WriteLine("2 -- Автору ");
        Console.WriteLine("3 -- Жанру");
        Console.WriteLine("0 -- Вернуться в основное меню");
    }


    public void findingBook()
    {
        bool cond = true;
        IEnumerable<Book> result1 = null;

        while (cond)
        {
            printFindingMenu();

            int var = 0;
            bool result = false;
            

            while (!result)
            {
                Console.WriteLine("Введите число выбранного пункта");
                result = int.TryParse(Console.ReadLine(), out var);
            }

            switch (var)
            {
                case 0: cond = false; break;
                case 1: 
                    string name = Console.ReadLine();
                    result1 = books.Where(b => b.name.Equals(name, StringComparison.OrdinalIgnoreCase));
                    foreach (Book findbook in result1)
                    {
                        findbook.printInfo();
                    }
                    break;
                case 2:
                    string nameAuthor = Console.ReadLine();
                    result1 = books.Where(b => b.author.Equals(nameAuthor, StringComparison.OrdinalIgnoreCase));
                    foreach (Book findbook in result1)
                    {
                        findbook.printInfo();
                    }
                    break;
                case 3:
                    Console.WriteLine("Выберите жанр: 0 - Classic, 1 - Detective, 2 - SciFi, 3 - Fantasy");
                    int genreChoice = int.Parse(Console.ReadLine());
                    Genre genre = (Genre)genreChoice;
                    result1 = books.Where(b => b.genre == genre);
                    foreach (Book findbook in result1)
                    {
                        findbook.printInfo();
                    }
                    break;
                default: Console.WriteLine("Некорректный ввод. Введите число."); break;
            }

            if (result1 == null) {
                Console.WriteLine("Такая книга не найдена");
            }

        }

    }


    public void sortingBooks()
    {

        bool cond = true;
        List<Book>sortedbooks = new List<Book>();

        while (cond)
        {
            printSortingMenu();

            int var = 0;
            bool result = false;


            while (!result)
            {
                Console.WriteLine("Введите число выбранного пункта");
                result = int.TryParse(Console.ReadLine(), out var);
            }

            switch (var)
            {
                case 0: cond = false; break;
                case 1:
                    books = books.OrderBy(b => b.name).ToList();
                    Console.WriteLine("Книги отсортированы по названию:");
                    printLibraryBooks();
                    break;
                case 2:
                    books = books.OrderBy(b => b.year).ToList();
                    Console.WriteLine("Книги отсортированы по году:");
                    printLibraryBooks();
                    break;
                case 3:
                    books = books.OrderBy(b =>b.ID).ToList();
                    Console.WriteLine("Книги отсортированы по ID:");
                    printLibraryBooks();
                    break;
                default: Console.WriteLine("Некорректный ввод. Введите число."); break;
            }

        }

    }

    void printSortingMenu()
    {
        Console.WriteLine("Какую сортировку книг применить?");
        Console.WriteLine("1 --- По названию");
        Console.WriteLine("2 --- По году");
        Console.WriteLine("3 --- По ID");
        Console.WriteLine("0 --- Вернуться в основное меню");
    }

    public void cheapExpensiveBooks()
    {
        List<Book> sortedPriceBooks = new List<Book>();
        sortedPriceBooks = books.OrderBy(b => b.price).ToList();
        Book cheapestB = sortedPriceBooks[0];
        Book expensiveB = sortedPriceBooks[sortedPriceBooks.Count-1];
        Console.WriteLine("Самая дорогая книга:");
        expensiveB.printInfo();
        Console.WriteLine("Самая дешевая книга:");
        cheapestB.printInfo();
        
    }

    public void groupAuthors()
    {
        var authorGroups = new Dictionary<string, int>();
        foreach (var book in books)
        {
            if (authorGroups.ContainsKey(book.author))
                authorGroups[book.author]++;
            else
                authorGroups[book.author] = 1;
        }

        Console.WriteLine("Количество книг по авторам:");
        foreach (var pair in authorGroups)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }


}



