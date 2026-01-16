using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarService
{
    internal class Program
    {
        static List<CarParts> carParts = Core.Context.CarParts.ToList();
        static List<CarServices> carServices = Core.Context.CarServices.ToList();
        static List<CarServicesCarParts> carServicesCarParts = Core.Context.CarServicesCarParts.ToList();

        int carsShoppingCounter = 0;

        static void Main(string[] args)
        {

            bool gaming = true;

            while (gaming)
            {
                int choice = mainMenu();

                switch(choice)
                {
                    case 1: 
                        Console.WriteLine("Ваш баланс: " + carServices[0].AmountOfMoney);
                        break;
                    case 2: availableParts(); break;
                    case 3: client(); break;
                    case 4: shopping(); break;
                    case 5: gaming = false; break;
                    default: break;
                }
            }

        }
        static int mainMenu()
        {
            Console.WriteLine("ГЛАВНОЕ МЕНЮ");
            Console.WriteLine("1. Проверить баланс");
            Console.WriteLine("2. Посмотреть детали в наличии");
            Console.WriteLine("3. Принять клиента");
            Console.WriteLine("4. Купить деталь");
            Console.WriteLine("5. Закончить игру");

            int choice = 0;
            bool result = false;
            while (!result)
            {
                Console.WriteLine("Введите число выбранного пункта");
                result = int.TryParse(Console.ReadLine(), out choice);

                if (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5)
                {
                    result = false;
                    Console.WriteLine("НЕКОРРЕКТНЫЙ ВВОД");
                }

            }

            return choice;
        }

        static int repairMenu()
        {
            Console.WriteLine("Ремонтируем? 1 - да, 2 - нет");

            int choice = 0;
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

            return choice;
        }

        static int orderDeatailChoice()
        {
            Console.WriteLine("Заказываем деталь? 1 - да, 2 - нет");

            int choice = 0;
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

            return choice;
        }

        static void availableParts()
        {
            foreach (CarParts carPart in carParts)
            {
                foreach (var carServicesCarPart in carServicesCarParts)
                {
                    if (carPart.ID == carServicesCarPart.CarPartID && carServicesCarPart.CarServiceID == 1)
                    {
                        Console.WriteLine(carPart.Name + " " + carServicesCarPart.QuantityOfPart);
                    }
                }
            }
        }

        static void client()
        {
            Random random = new Random();
            int index = random.Next(carParts.Count);

            Console.WriteLine("Новый клиент");
            Console.WriteLine($"Сломанная деталь: {carParts[index].Name}");
            double finalPrice = (double)carParts[index].Price + (double)carParts[index].Price * 0.10;
            Console.WriteLine($"Стоимость ремонта: {finalPrice}\n");

            Console.WriteLine("ДЕТАЛИ, ИМЕЮЩИЕСЯ НА СКЛАДЕ:");
            availableParts();

            //ДОЗАКАЗАТЬ ДЕТАЛЬ

            if (orderDeatailChoice() == 1)
            {
                shopping();
            }


            int choice = repairMenu();

            CarServicesCarParts foundPart = carServicesCarParts.FirstOrDefault(part => part.CarPartID == carParts[index].ID);


            switch (choice)
            {
                case 1:
                    if (foundPart.QuantityOfPart > 0)
                    {
                        foreach (var item in carServicesCarParts)
                        {
                            if(carParts[index].ID == item.ID)
                            {
                                item.QuantityOfPart -= 1; //убрали деталь
                            }
                        }
                        carServices[0].AmountOfMoney += (int)finalPrice;
                        Console.WriteLine("Машина отремонтирована");
                        Console.WriteLine($"Ваш баланс - {carServices[0].AmountOfMoney}");
                    } else if (foundPart.QuantityOfPart == 0)
                    {
                        Console.WriteLine("На складе нет детали!!! Меняем случайную деталь!!!");


                        bool findingDetailInStock = false;

                        while(!findingDetailInStock)
                        {
                            int indexRandomRepair = random.Next(carParts.Count);
                            CarServicesCarParts randomPart = carServicesCarParts.FirstOrDefault(part => part.CarPartID == carParts[indexRandomRepair].ID);

                            if (randomPart.QuantityOfPart > 0)
                            {
                                findingDetailInStock = true;

                                foreach (var item in carServicesCarParts)
                                {
                                    if (carParts[indexRandomRepair].ID == item.ID)
                                    {
                                        item.QuantityOfPart -= 1; //убрали деталь
                                    }
                                }
                                carServices[0].AmountOfMoney += (int)finalPrice;
                                Console.WriteLine("Машина отремонтирована рандомной деталью");
                                Console.WriteLine($"Ваш баланс - {carServices[0].AmountOfMoney}");

                                carServices[0].AmountOfMoney -= 2000; //штраф
                                Console.WriteLine("Клиент недоволен!!! Вас отштрафовали!!!");
                                Console.WriteLine($"Ваш баланс - {carServices[0].AmountOfMoney}");

                            }

                        }

                    }
                    break;
                case 2:
                    Console.WriteLine("Нужная деталь отсутствует на складе. Мы вынуждены отказать в обслуживании :(");
                    carServices[0].AmountOfMoney -= 500; //штраф
                    Console.WriteLine("Вас оштрафовали за отказ в обслуживании!!!");
                    Console.WriteLine($"Ваш баланс - {carServices[0].AmountOfMoney}");
                    break;
                    
            }
            

        }

        static void shopping()
        {
            Console.WriteLine("ДОСТУПНЫЕ ДЛЯ ЗАКАЗА ДЕТАЛИ:");
            foreach (CarParts carPart in carParts)
            {
                    Console.WriteLine($"ID: {carPart.ID} NAME: {carPart.Name}");
            }

            int choice = 0;
            bool result = false;
            while (!result)
            {
                Console.WriteLine("Напишите ID детали, которую хотите заказать:");
                result = int.TryParse(Console.ReadLine(), out choice);

                if (choice == 4 || choice <= 0 || choice > 12)
                {
                    result = false;
                    Console.WriteLine("НЕКОРРЕКТНЫЙ ВВОД");
                }
            }

            // проверка на наличие нужной суммы денег

            foreach (CarParts carPart in carParts)
            {
                if(carPart.ID==choice)
                {
                    if (carServices[0].AmountOfMoney >= carPart.Price)
                    {
                        // заказываем
                        carServices[0].AmountOfMoney -= carPart.Price;
                        Console.WriteLine("Оплата прошла. Деталь будет доставлена через 2 машины.");
                        // прописать доставку детали
                    }
                    else
                    {
                        Console.WriteLine("Недостаточно средств на балансе!!!");
                    }
                }
            }


            // какие детали доступны и сколько они стоят, и купить нужное количество.
            // Деньги за покупку сразу списываются с вашего баланса,
            // но детали появляются не сразу, а только спустя 2 машины (не важно, обслужена машина или нет).
        }
    }

}
