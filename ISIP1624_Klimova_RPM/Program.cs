// See https://aka.ms/new-console-template for more information


void printMenu()
{
    Console.WriteLine();
    Console.WriteLine("УПРАВЛЕНИЕ ТРАТАМИ");
    Console.WriteLine("1. Вывод данных");
    Console.WriteLine("2. Статистика (среднее, максимальное, минимальное, сумма)");
    Console.WriteLine("3. Сортировка по цене (пузырьковая сортировка)");
    Console.WriteLine("4. Конвертация валюты (пользователь вводит курс или выбирает из списка).");
    Console.WriteLine("5. Поиск по названию");
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
int[] money = new int[size];

Console.WriteLine("Введите название и стоимость через ; ");

for (int i = 0; i < amountOfOperations; i++)
{
    input = Console.ReadLine();

    string[] splitInput = input.Split(new char[] { ';' });
    operations[i] = splitInput[0];
    money[i] = int.Parse(splitInput[1]); 

}


void coutData()
{
    for(int i = 0; i < amountOfOperations; i++)
    {
        Console.WriteLine(operations[i] + ' ' + money[i]);
    }
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
        case 2: ; break;
        case 3: ; break;
        case 4: ; break;
        case 5: ; break;
        default: Console.WriteLine("Что-то пошло не так. Попробуйте ещё раз"); break;
    }
}
