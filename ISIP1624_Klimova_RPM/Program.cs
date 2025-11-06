// See https://aka.ms/new-console-template for more information


using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;


Stack<TextStatsNumbers> alltexts = new Stack<TextStatsNumbers>();



TextStatsNumbers textEx = new TextStatsNumbers();
bool condition = true;

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

    switch(var)
    {
        case 0: condition = false; break;   
        case 1: textInput(); break;
        case 2: stats(); break;
        case 3: saveTextStats(); break;
        case 4: printStatsPreviosTexts(); break;
            default: Console.WriteLine("Некорректный ввод. Введите число."); break;
    }

}

void printMainMenu()
{
    Console.WriteLine("Приложение работы с текстом");
    Console.WriteLine("1. Ввести текст для подсчет статистики");
    Console.WriteLine("2. Посчитать статистику по последнему введенному тексту");
    Console.WriteLine("3. Сохранить статистику по последнему введенному тексту");
    Console.WriteLine("4. Вывести статистику по последнему сохраненному тексту в стэке"); 
    Console.WriteLine("0. Закончить работу");
}


void textInput()
{

    string stopSymbol = "#"; // Стоп-символ
    StringBuilder inputBuilder = new StringBuilder();

    Console.WriteLine($"Введите текст. Чтобы завершить ввод, введите '{stopSymbol}' на отдельной строке.");

    while (true)
    {
        string line = Console.ReadLine();

        if (line == stopSymbol)
        {
            break; // завершение ввода при встрече со стоп-символом
        }

        inputBuilder.Append(line);
    }

    string resultText = inputBuilder.ToString();


    if(resultText.Length < 100)
    {
        Console.WriteLine("Введено менее 100 символов. Попробуйте ещё раз.");
    } else
    {
        Console.WriteLine("Ввод успешно завершен. Введенный текст:");
        Console.WriteLine(resultText);
        textEx.text = resultText;
    }

}

void saveTextStats()
{
    alltexts.Push(textEx);
}

void printStatsPreviosTexts()
{
    Console.WriteLine("Статистика по всему введенному тексту");
    Console.WriteLine($"Текст: {alltexts.Peek().text}");
    Console.WriteLine($"Кол-во слов в веденном тексте: {alltexts.Peek().wordsN}");
    Console.WriteLine($"Кол-во предложений в веденном тексте: {alltexts.Peek().sentencesN}");
    Console.WriteLine($"Количество гласных букв: {alltexts.Peek().vowelsN}");
    Console.WriteLine($"Количество согласных букв: {alltexts.Peek().consonantN}");
    Console.WriteLine($"Самое короткое слово: {alltexts.Peek().shortestWord}");
    Console.WriteLine($"Самое длинное слово: {alltexts.Peek().longestWord}");
    Console.WriteLine("Статистика по частоте встречаемости букв:");
    foreach (var item in alltexts.Peek().lettersN)
    {
        Console.WriteLine($"{item.Key}: {item.Value}");
    }
}

void printStatsMenu()
{
    Console.WriteLine("1. Подсчёт количества слов в тексте");
    Console.WriteLine("2. Подсчёт количества предложений");
    Console.WriteLine("3. Подсчёт количества гласных и согласных букв");
    Console.WriteLine("4. Поиск самого короткого и самого длинного слова");
    Console.WriteLine("5. Создание статистики по частоте встречаемости каждой буквы");
    Console.WriteLine("6. Вывести всю статистику разом");
    Console.WriteLine("7. Посчитать сразу всё и вывести");
    Console.WriteLine("0. Закончить работу с текстом и вернуться в основное меню");
}
void stats()
{
    bool condition2 = true;

    if (string.IsNullOrWhiteSpace(textEx.text))
    {
        Console.WriteLine("Текст пустой. Введите текст ещё раз");
        condition2 = false;

    }



    while (condition2)
    {
        printStatsMenu();

        int var2 = 0;
        bool result2 = false;
        while (!result2)
        {
            Console.WriteLine("Введите число выбранного пункта");
            result2 = int.TryParse(Console.ReadLine(), out var2);
        }

        switch (var2)
        {
            case 0: condition2 = false; break;
            case 1: wordsCounting(); break;
            case 2: sentencesCounting(); break;
            case 3: vowelsConsonantLetters(); break;
            case 4: findingShortestLongestWord(); break;
            case 5: lettersFrequency(); break;
            case 6: printAllStats(); break;
            case 7:
                wordsCounting();
                sentencesCounting();
                vowelsConsonantLetters();
                findingShortestLongestWord();
                lettersFrequency();
                printAllStats(); 
                break;
            default: Console.WriteLine("Некорректный ввод. Введите число."); break;
        }
    }
}

void wordsCounting()
{
    if (string.IsNullOrWhiteSpace(textEx.text))
    {
        Console.WriteLine("Текст пустой. Введите текст ещё раз");
        
    } else
    {
        char[] delimiters = { ' ', '\t', '\n', '\r', ',', '.', '!', '?', ';', ':', '-', '\"', '\'' };
        textEx.words = textEx.text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        textEx.wordsN = textEx.words.Length;
        Console.WriteLine($"Кол-во слов в веденном тексте: {textEx.wordsN}");
    }



}

void sentencesCounting()
{

    if (string.IsNullOrWhiteSpace(textEx.text))
    {
        Console.WriteLine("Текст пустой. Введите текст ещё раз");

    }
    else
    {
        char[] delimiters = { '.', '!', '?'};
        string[] sentences = textEx.text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        textEx.sentencesN = sentences.Length;
        Console.WriteLine($"Кол-во предложений в веденном тексте: {textEx.sentencesN}");
    }

}

void vowelsConsonantLetters()
{
    char[] vowels = { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я',
                      'А', 'Е', 'Ё', 'И', 'О', 'У', 'Ы', 'Э', 'Ю', 'Я' };
    char[] consonants = { 'б','в','г','д','ж','з','й','к','л','м','н','п','р','с','т','ф','х','ц','ч','ш','щ',
                          'Б','В','Г','Д','Ж','З','Й','К','Л','М','Н','П','Р','С','Т','Ф','Х','Ц','Ч','Ш','Щ' };

    int vowelsCount = 0;
    int consonantCount = 0;

    foreach (char c in textEx.text)
    {
        if (vowels.Contains(c))
        {
            vowelsCount++;
        }
        else if (consonants.Contains(c))
        {
            consonantCount++;
        }
    }

    Console.WriteLine($"Количество гласных букв: {vowelsCount}");
    Console.WriteLine($"Количество согласных букв: {consonantCount}");

    textEx.consonantN = consonantCount;
    textEx.vowelsN = vowelsCount;

}

void findingShortestLongestWord()
{
    if (textEx.words.Length == 0)
    {
        Console.WriteLine("В тексте нет слов");
        return;
    }

    string shortestWord = textEx.words[0];
    string longestWord = textEx.words[0];

    foreach (string word in textEx.words)
    {
        if (word.Length < shortestWord.Length)
        {
            shortestWord = word;
        }
        if (word.Length > longestWord.Length)
        {
            longestWord = word;
        }
    }

    Console.WriteLine($"Самое короткое слово: {shortestWord}");
    Console.WriteLine($"Самое длинное слово: {longestWord}");

    textEx.shortestWord = shortestWord;
    textEx.longestWord = longestWord;
}


void lettersFrequency()
{
    Dictionary<char, int> letterCounts = new Dictionary<char, int>();

    string text = textEx.text.ToLower();

    foreach (char c in text)
    {
        if (char.IsLetter(c))
        {
            if (letterCounts.ContainsKey(c))
            {
                letterCounts[c]++;
            }
            else
            {
                letterCounts[c] = 1;
            }
        }
    }

    List<KeyValuePair<char, int>> list = new List<KeyValuePair<char, int>>(letterCounts);

    // метод пузырька
    for (int i = 0; i < list.Count - 1; i++)
    {
        for (int j = i + 1; j < list.Count; j++)
        {
            if (list[j].Value > list[i].Value)
            {
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }

    Console.WriteLine("Статистика по частоте встречаемости букв:");
    foreach (var item in list)
    {
        Console.WriteLine($"{item.Key}: {item.Value}");
    }

    textEx.lettersN = list;
}

void printAllStats()
{
    Console.WriteLine("Статистика по всему введенному тексту");
    Console.WriteLine($"Текст: {textEx.text}");
    Console.WriteLine($"Кол-во слов в веденном тексте: {textEx.wordsN}");
    Console.WriteLine($"Кол-во предложений в веденном тексте: {textEx.sentencesN}");
    Console.WriteLine($"Количество гласных букв: {textEx.vowelsN}");
    Console.WriteLine($"Количество согласных букв: {textEx.consonantN}");
    Console.WriteLine($"Самое короткое слово: {textEx.shortestWord}");
    Console.WriteLine($"Самое длинное слово: {textEx.longestWord}");
    Console.WriteLine("Статистика по частоте встречаемости букв:");
    foreach (var item in textEx.lettersN)
    {
        Console.WriteLine($"{item.Key}: {item.Value}");
    }

}

struct TextStatsNumbers
{
    public string text;
    public int wordsN;
    public string[] words;
    public string shortestWord;
    public string longestWord;
    public int sentencesN;
    public int vowelsN;
    public int consonantN;
    public List<KeyValuePair<char, int>> lettersN;



} // сохранение всей статистики в список 