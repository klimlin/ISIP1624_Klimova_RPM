
using ISIP1624_Klimova_RPM.Classes;

Console.WriteLine("Начать игру? 1 - да, 2 - нет");

int choice = 3;
bool result = false;
while (!result)
{
    Console.WriteLine("Введите число выбранного пункта");
    result = int.TryParse(Console.ReadLine(), out choice);

    if (choice != 1 && choice != 2)
    {
        result = false;
        Console.WriteLine("НЕКОРРЕКТНЫЙ ВВОД");
    }

}

bool game = false;

if (choice == 1)
{
    game = true;
}

while (game)
{
    Game game1 = new Game();
    game = game1.HodIgry();
}

Console.WriteLine("ИГРА ЗАКОНЧЕНА");

