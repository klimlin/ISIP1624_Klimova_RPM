using System;
using System.Collections.Generic;
using System.Linq;
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

        static void Main(string[] args)
        {

            bool gaming = true;

            while (true)
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

            Console.WriteLine("ВЫВЕСТИ ВСЕ ДЕТАЛИ ДОДЕЛАТЬ ПОТОМ");
            //ДОЗАКАЗАТЬ ДЕТАЛЬ

            Console.WriteLine("Хотите дозаказать деталь?");

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
                                item.QuantityOfPart -= 1;
                            }
                        }
                        carServices[0].AmountOfMoney += (int)finalPrice;
                        Console.WriteLine("Машина отремонтирована");
                        Console.WriteLine($"Ваш баланс - {carServices[0].AmountOfMoney}");
                    }
                    break;
                case 2:
                    Console.WriteLine("Нужная деталь отсутствует на складе. Мы вынуждены отказать в обслуживании");
                    carServices[0].AmountOfMoney -= 500; //штраф
                    Console.WriteLine("Вас отштрафовали!!!");
                    Console.WriteLine($"Ваш баланс - {carServices[0].AmountOfMoney}");
                    break;
                    
            }
            
            // Вы смотрите на свой склад и решаете - есть ли у вас нужная запчасть.
            // Если есть, вы меняете её и получаете деньги от клиента.
            // Если нет - можете отказать клиенту, но тогда придётся заплатить штраф за отказ в обслуживании. 

            // Будьте внимательны! Если вы примите заказ, но нужной детали на складе не будет,
            // то будет произведена замена другой случайной детали, которая есть на складе,
            // и тогда клиент вернётся очень недовольным и
            // вы будете обязаны возместить ему ущерб - это выйдет дороже чем просто отказать.


        }

        static void shopping()
        {
            // какие детали доступны и сколько они стоят, и купить нужное количество.
            // Деньги за покупку сразу списываются с вашего баланса,
            // но детали появляются не сразу, а только спустя 2 машины (не важно, обслужена машина или нет).
        }
    }

}
