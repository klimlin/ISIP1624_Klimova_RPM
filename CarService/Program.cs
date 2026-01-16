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

        // прописать сохранение данных в БД

        static List<CarParts> carParts = Core.Context.CarParts.ToList();
        static List<CarServices> carServices = Core.Context.CarServices.ToList();
        static List<CarServicesCarParts> carServicesCarParts = Core.Context.CarServicesCarParts.ToList();

        class DeliveryDetail
        {
            public CarParts carPart;
            public int carCounter;
            public int amount;

            public DeliveryDetail(CarParts carPart, int carCounter, int amount)
            {
                this.carPart = carPart;
                this.carCounter = carCounter;
                this.amount = amount;
            }

            public void PrintDetails()
            {
                Console.WriteLine($"Предмет: {carPart.Name}, Количество: {amount}, Ск машин до доставки: {carCounter}");
            }
        }

        static List<DeliveryDetail> deliveryDetailList = new List<DeliveryDetail>();

        static void Main(string[] args)
        {

            bool gaming = true;

            while (gaming)
            {
                int choice = mainMenu();

                if (carServices[0].AmountOfMoney < 0)
                {
                    choice = 8;
                }

                switch(choice)
                {
                    case 1: 
                        Console.WriteLine("Ваш баланс: " + carServices[0].AmountOfMoney);
                        break;
                    case 2: availableParts(); break;
                    case 3: client(); break;
                    case 4: shopping(); break;
                    case 5: delivery(); break;
                    case 6: save(); break;
                    case 7: gaming = false; break;
                    case 8:
                        Console.WriteLine("ВЫ УШЛИ В МИНУС!\n GAME OVER");
                        gaming = false; break;
                    default: break;
                }
            }

        }

        static void save()
        {
            Core.Context.SaveChanges();
        }

        static void delivery()
        {
            foreach (var item in deliveryDetailList)
            {
                item.PrintDetails();
            }
        }

        static int mainMenu()
        {
            Console.WriteLine("\nГЛАВНОЕ МЕНЮ");
            Console.WriteLine("1. Проверить баланс");
            Console.WriteLine("2. Посмотреть детали в наличии");
            Console.WriteLine("3. Принять клиента");
            Console.WriteLine("4. Купить деталь");
            Console.WriteLine("5. Проверить детали на доставку");
            Console.WriteLine("6. Сохранить результат в БД");
            Console.WriteLine("7. Закончить игру");

            int choice = 0;
            bool result = false;
            while (!result)
            {
                Console.WriteLine("Введите число выбранного пункта");
                result = int.TryParse(Console.ReadLine(), out choice);

                if (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5 && choice != 6 && choice != 7)
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

            // отнимаем 1 машину в списке для доставки

            for (int i = 0; i < deliveryDetailList.Count; i++)
            {
                var item = deliveryDetailList[i];
                item.carCounter -= 1;
                deliveryDetailList[i] = item; // обновляем элемент в списке
            }

            Console.WriteLine("\nНовый клиент");
            Console.WriteLine($"Сломанная деталь: {carParts[index].Name}");
            double finalPrice = (double)carParts[index].Price * 1.10;
            Console.WriteLine($"Стоимость ремонта: {finalPrice}");

            Console.WriteLine("\nДЕТАЛИ, ИМЕЮЩИЕСЯ НА СКЛАДЕ:");
            availableParts();
            Console.WriteLine();
            //ДОЗАКАЗАТЬ ДЕТАЛЬ

            if (orderDeatailChoice() == 1)
            {
                shopping();
            }

            Console.WriteLine();
            int choice = repairMenu();

            CarServicesCarParts foundPart = carServicesCarParts.FirstOrDefault(part => part.CarPartID == carParts[index].ID);

            switch (choice)
            {
                case 1:
                    if (foundPart.QuantityOfPart > 0)
                    {
                        for (int j = 0; j < carServicesCarParts.Count; j++)
                        {
                            if (carServicesCarParts[j].CarPartID == foundPart.CarPartID)
                            {
                                carServicesCarParts[j].QuantityOfPart -= 1;
                                
                            }
                        }

                        carServices[0].AmountOfMoney += (int)finalPrice;
                        Console.WriteLine("\nМашина отремонтирована");
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

                                for (int i = 0; i < carServicesCarParts.Count; i++)
                                {
                                    if (carParts[indexRandomRepair].ID == carServicesCarParts[i].CarPartID)
                                    {
                                        carServicesCarParts[i].QuantityOfPart -= 1;//убрали деталь
                                    }
                                }
                                carServices[0].AmountOfMoney += (int)finalPrice;
                                Console.WriteLine("\nМашина отремонтирована рандомной деталью");
                                Console.WriteLine($"Ваш баланс - {carServices[0].AmountOfMoney}");

                                carServices[0].AmountOfMoney -= 2000; //штраф
                                Console.WriteLine("Клиент недоволен!!! Вас отштрафовали!!!");
                                Console.WriteLine($"Ваш баланс - {carServices[0].AmountOfMoney}");

                            }

                        }

                    }
                    break;
                case 2:
                    Console.WriteLine("\nНужная деталь отсутствует на складе. Мы вынуждены отказать в обслуживании :(");
                    carServices[0].AmountOfMoney -= 500; //штраф
                    Console.WriteLine("Вас оштрафовали за отказ в обслуживании!!!");
                    Console.WriteLine($"Ваш баланс - {carServices[0].AmountOfMoney}");
                    break;
                    
            }

            // проверяем доставку деталей 

            for (int i = 0; i < deliveryDetailList.Count; i++)
            {
                if (deliveryDetailList[i].carCounter == 0)
                {
                    for(int j = 0; j < carServicesCarParts.Count; j++)
                    {
                        if (carServicesCarParts[j].CarPartID == deliveryDetailList[i].carPart.ID)
                        {
                            carServicesCarParts[j].QuantityOfPart += deliveryDetailList[i].amount;
                        }
                    }

                    Console.WriteLine($"\nДеталь {deliveryDetailList[i].carPart.Name} доставлена в количестве {deliveryDetailList[i].amount}");
                }
            }

            // удаляем доставленные элементы из списка на доставку

            for (int i = 0; i < deliveryDetailList.Count; i++)
            {
                if (deliveryDetailList[i].carCounter <= 0)
                {
                    deliveryDetailList.RemoveAt(i);
                }
            }

        }

        static void shopping()
        {
            Console.WriteLine("\nДОСТУПНЫЕ ДЛЯ ЗАКАЗА ДЕТАЛИ:");

            foreach (CarParts carPart in carParts)
            {
                    Console.WriteLine($"ID: {carPart.ID} NAME: {carPart.Name} PRICE: {carPart.Price}");
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

            int choice1 = 0;
            bool result1 = false;
            while (!result1)
            {
                Console.WriteLine("Сколько комплектов заказываем?");
                result1 = int.TryParse(Console.ReadLine(), out choice1);

                if (choice1 <= 0)
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
                    if (carServices[0].AmountOfMoney >= carPart.Price*choice1)
                    {
                        
                        // заказываем
                        carServices[0].AmountOfMoney -= carPart.Price*choice1;
                        Console.WriteLine("Оплата прошла. Покупка будет доставлена через 2 машины.");

                        DeliveryDetail newItem = new DeliveryDetail(carPart, 2, choice1);
                        deliveryDetailList.Add(newItem);

                    }
                    else
                    {
                        Console.WriteLine("Недостаточно средств на балансе!!!");
                    }
                }
            }

        }
    }

}
