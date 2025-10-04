// See https://aka.ms/new-console-template for more information


using static System.Runtime.InteropServices.JavaScript.JSType;

void printMenu()
{
    Console.WriteLine();
    Console.WriteLine("УПРАВЛЕНИЕ ТРАТАМИ");
    Console.WriteLine("1. Вывод данных");
    Console.WriteLine("2. Статистика (среднее, максимальное, минимальное, сумма)");
    Console.WriteLine("3. Сортировка по цене (пузырьковая сортировка)");
    Console.WriteLine("4. Конвертация валюты (пользователь вводит курс или выбирает из списка).");
    Console.WriteLine("5. Конвертация валюты (пользователь выбирает из списка)");
    Console.WriteLine("6. Поиск по названию");
    Console.WriteLine("0. Выход");
}



Console.WriteLine("Введите количество операций: ");

string input = Console.ReadLine();
int size = 40;

int amountOfOperations;
bool result1 = int.TryParse(input, out amountOfOperations);

if (result1 == true && amountOfOperations > 1 && amountOfOperations < 41)
    Console.WriteLine($"Ввод корректен. Кол-во операций: {amountOfOperations}");
else
{
    Console.WriteLine("Ввод некорректен.");
    Environment.Exit(0);
}


string[] operations = new string[size];
double[] money = new double[size];

Console.WriteLine("Введите название и стоимость через ; ");

for (int i = 0; i < amountOfOperations; i++)
{
    input = Console.ReadLine();

    string[] splitInput = input.Split(new char[] { ';' });
    operations[i] = splitInput[0];
    money[i] = double.Parse(splitInput[1]); 

}


void coutData()
{
    for(int i = 0; i < amountOfOperations; i++)
    {
        Console.WriteLine(operations[i] + ' ' + money[i]);
    }
}

void statistica()
{
    double max = money[0];
    double min = money[0];
    double average = 0;
    double sum = 0;

    for(int i = 0; i < amountOfOperations; i++)
    {
        sum += money[i];
        if(max < money[i]) max = money[i];
        if(min > money[i]) min = money[i];
    }
    average = sum / amountOfOperations;

    Console.WriteLine("Статистика");
    Console.WriteLine("Минимальное значение: " + min);
    Console.WriteLine("Максимальное значение: " + max);
    Console.WriteLine("Сумма: " + sum);
    Console.WriteLine("Среднее значение: " + average);

};

void bubbleSort()
{
    for (int i = 1; i < amountOfOperations; i++)
    {
        for (int j = i; j > 0 && money[j - 1] > money[j]; j--)
        {
            // прописать метод swap
            double tmp = money[j-1];
            money[j-1] = money[j];
            money[j] = tmp;

            string tmp2 = operations[j - 1];
            operations[j - 1] = operations[j];
            operations[j] = tmp2;

        }
    }
}

void convertCurrencyYourself()
{
    Console.WriteLine("Введите нужный курс рубля: ");
    input = Console.ReadLine();
    int rate = 1;
    bool res = int.TryParse(input, out rate);

    for (int i = 0; i < amountOfOperations; i++)
    {

        money[i] *= rate;
        Console.WriteLine(operations[i] + ' ' + money[i]);
    }

}


void printCurrencyMenu()
{
    Console.WriteLine("1. Доллар 81 руб.");
    Console.WriteLine("2. Евро 96 руб.");
    Console.WriteLine("3. Юани 11.4 руб.");
    Console.WriteLine("0. Выйти в основное меню");

}

void mathRate(double rating)
{
    for (int i = 0; i < amountOfOperations; i++)
    {

        money[i] /= rating;
        Console.WriteLine(operations[i] + ' ' + money[i]);
    }
}

void convertCurrencyMenu()
{
    

    int t = 0;
    bool result = false;
    bool flag = true;

    while (flag)
    {
        printCurrencyMenu();

        do
        {
            Console.WriteLine("Выберите действие (цифрой): ");
            input = Console.ReadLine();
            result = int.TryParse(input, out t);
        } while (result == false);


        switch (t)
        {
            case 0: flag = false; break;
            case 1: mathRate(81); break;
            case 2: mathRate(96); break;
            case 3: mathRate(11.4); break;
            default: Console.WriteLine("Что-то пошло не так. Попробуйте ещё раз"); break;
        }
    }



}

void findName()
{

    Console.WriteLine("Что ищете?");
    string nameToFind = Console.ReadLine();
    bool fla = true;

    for (int i = 0; i < amountOfOperations; i++)
    {
        int indexOfFind = operations[i].IndexOf(nameToFind);

        if (indexOfFind != -1)
        {
            fla = false;
            Console.WriteLine("Найдено! " + operations[i] + ' ' + money[i]);
            break;
        }

    }

   if (fla) Console.WriteLine("Такой операции нет");


}


int v = 0;
bool result = false;
bool flag = true;

while (flag)
{
    printMenu();

    do
    {
        Console.WriteLine("Выберите действие (цифрой): ");
        input = Console.ReadLine();
        result = int.TryParse(input, out v);
    } while (result == false);


    switch (v)
    {
        case 0: flag = false; break;
        case 1: coutData(); break;
        case 2: statistica(); break;
        case 3: bubbleSort(); break;
        case 4: convertCurrencyYourself(); break;
        case 5: convertCurrencyMenu(); break;
        case 6: findName(); break;
        default: Console.WriteLine("Что-то пошло не так. Попробуйте ещё раз"); break;
    }
}
