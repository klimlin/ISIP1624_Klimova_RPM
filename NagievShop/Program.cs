using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagievShop
{
    internal class Program
    {
        static List<Item> items = Core.Context.Item.ToList();
        static List<Cart> carts = Core.Context.Cart.ToList();
        static List<CartItem> cartItems = Core.Context.CartItem.ToList();
        static List<Order> orders = Core.Context.Order.ToList();
        static List<ItemOrder> itemOrders = Core.Context.ItemOrder.ToList();
        static List<PickUpPoint> pickUpPoints = Core.Context.PickUpPoint.ToList();
        static List<User> users = Core.Context.User.ToList();
        

        // Приветсвие
        // Зарегистрироваться. Войти. Просмотреть товары без регистрации
        // Регистрация с подтверждением пароля (пароль 6 символов)


        static void Main(string[] args)
        {

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("МАГАЗИН");
                Console.WriteLine("1. Регистрация\n2. Вход\n3. Просмотр товаров\n4. Выход");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Register(); break;
                    case "2":
                        User user = Login();
                        if (user != null) UserMenu(user);
                        break;
                    case "3": ShowItems(); break;
                    case "4": exit = true; break;
                    default: Console.WriteLine("Некорректный ввод. Попробуйте снова."); break;
                }
            }

        }

        static void UserMenu(User user)
        {

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("МЕНЮ ЗАРЕГИСТРИРОВАННОГО ПОЛЬЗОВАТЕЛЯ");
                Console.WriteLine("1. Просмотр товаров");
                Console.WriteLine("2. Добавить товар в корзину");
                Console.WriteLine("3. Просмотр корзины");
                Console.WriteLine("4. Заказать 1 товар");
                Console.WriteLine("5. Заказать все товары в корзине и очистить корзину");
                Console.WriteLine("6. Мои заказы");
                Console.WriteLine("7. Выход");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ShowItems(); break;
                    case "2": AddItemToCart(user); break;
                    case "3": ShowCart(user); break;
                    case "4": OrderSingleItem(user); break;
                    case "5": ; break;
                    case "6": ShowUserOrders(user); break;
                    case "7": exit = true; break;
                    default: Console.WriteLine("Некорректный ввод. Попробуйте снова."); break;
                }
            }
        }

        // ОСТАЛАСЬ ПЯТАЯ ФУНКЦИЯ!!!

        static void ShowUserOrders(User user)
        {
            // Получаем все заказы текущего пользователя и сортируем по дате (от новых к старым)
            var userOrders = orders
                .Where(o => o.UserID == user.ID)
                .OrderByDescending(o => o.Date)
                .ToList();

            if (userOrders.Count == 0)
            {
                Console.WriteLine("У вас нет заказов.");
                return;
            }

            foreach (var order in userOrders)
            {
                Console.WriteLine($"ID: {order.ID}");
                Console.WriteLine($"Дата заказа: {order.Date}");
                // Предположим, что есть свойство PickUpPointID
                var pickUpPoint = pickUpPoints.FirstOrDefault(p => p.ID == order.PickUpPointID);
                Console.WriteLine($"Пункт выдачи: {pickUpPoint.Adress}");
                Console.WriteLine($"Общая сумма: {order.Purchase:F2} руб.");

                // Получаем связанные товары
                var itemsInOrder = itemOrders.Where( u=> u.OrderID == order.ID).ToList();

                Console.WriteLine("Товары в заказе:");
                foreach (var itemOrder in itemsInOrder)
                {
                    var item = items.FirstOrDefault(i => i.ID == itemOrder.ItemID);
                    if (item != null)
                    {
                        Console.WriteLine($"- {item.Name} | Кол-во: {itemOrder.Quantity} | Цена за единицу: {item.Price:F2} руб.");
                    }
                }
                Console.WriteLine(new string('-', 40));
            }
        }

        static void OrderSingleItem(User user)
        {
            ShowItems();

            Console.WriteLine("Введите ID товара, который хотите заказать:");
            string itemIdInput = Console.ReadLine();

            if (!int.TryParse(itemIdInput, out int itemId))
            {
                Console.WriteLine("Некорректный ID товара. Попробуйте снова.");
                return;
            }

            Item itemToOrder = items.FirstOrDefault(i => i.ID == itemId);
            if (itemToOrder == null)
            {
                Console.WriteLine("Товар не найден.");
                return;
            }

            Console.WriteLine("Введите количество: ");
            string quantityInput = Console.ReadLine();
            int quantity;

            if (!int.TryParse(quantityInput, out quantity))
            {
                Console.WriteLine("Некорректное количество. Пожалуйста, введите число.");
                return;
            }

            // pickuppointID
            int adressId = 0;

            // Выводим список всех пунктов выдачи
            Console.WriteLine("Доступные пункты выдачи:");
            foreach (var point in pickUpPoints)
            {
                Console.WriteLine($"ID: {point.ID} Адрес: {point.Adress}");
            }

            // Запрашиваем у пользователя ввод ID
            Console.WriteLine("Введите ID пункта выдачи:");
            string input = Console.ReadLine();

            // Проверяем и парсим ввод
            if (int.TryParse(input, out adressId))
            {
                // Ищем пункт по ID
                var selectedPoint = pickUpPoints.FirstOrDefault(p => p.ID == adressId);
                if (selectedPoint == null)
                {
                    Console.WriteLine("Пункт с таким ID не найден.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод ID.");
                return;
            }

            // Создаем заказ
            Order newOrder = new Order
            {
                UserID = user.ID,
                Date = DateTime.Now,
                PickUpPointID = adressId,
                Purchase = quantity * itemToOrder.Price
            };
            orders.Add(newOrder); // нужно ли это писать?
            Core.Context.Order.Add(newOrder);
            Core.Context.SaveChanges();

            // Нужно ли связывать если товар 1? связывать товар с заказом через таблицу ItemOrder 
            //ItemOrder itemOrder = new ItemOrder
            //{
            //    OrderID = newOrder.ID,
            //    ItemID = itemToOrder.ID,
            //    Quantity = 1,
            //    Purchase = quantity * itemToOrder.Price
            //};
            //Core.Context.ItemOrder.Add(itemOrder);
            //Core.Context.SaveChanges();

            Console.WriteLine($"Заказ на товар '{itemToOrder.Name}' оформлен успешно!");
        }

        static void AddItemToCart(User user)
        {
            ShowItems();

            int targetitemId = -1;
            int quantity;

            Console.WriteLine("Напишите ID товара, который хотите приобрести: ");
            string productId = Console.ReadLine();

            Console.WriteLine("Введите количество: ");
            string quantityInput = Console.ReadLine();

            if (int.TryParse(quantityInput, out quantity) && int.TryParse(productId, out targetitemId))
            {
                Console.WriteLine($"Вы выбрали товар с ID: {productId} и количество: {quantity}");
            }
            else
            {
                if (!int.TryParse(productId, out targetitemId))
                {
                    Console.WriteLine("Некорректный ID товара. Пожалуйста, введите число.");
                }
                if (!int.TryParse(quantityInput, out quantity))
                {
                    Console.WriteLine("Некорректное количество. Пожалуйста, введите число.");
                }

                return;
            }

            // есть ли корзина у пользователя
            var userCart = carts.FirstOrDefault(c => c.UserID == user.ID);
            if (userCart == null)
            {
                // создаем новую корзину
                userCart = new Cart
                {
                    UserID = user.ID,
                    ChangeDate = DateTime.Now
                };
                carts.Add(userCart);
                // как именно сохранять??
                Core.Context.Cart.Add(userCart);
                Core.Context.SaveChanges(); 
            }

            // находим товар по ItemID
            var item = items.FirstOrDefault(i => i.ID == targetitemId);
            if (item == null)
            {
                Console.WriteLine("Товар не найден.");
                return;
            }

            // есть ли этот товар уже в корзине
            var existingCartItem = cartItems.FirstOrDefault(u => u.CartID == userCart.ID && u.ItemID == targetitemId);

            if (existingCartItem != null)
            {
                // обновляем количество и сумму
                existingCartItem.Quantity += quantity;
                existingCartItem.Purchase = existingCartItem.Quantity * item.Price;
            }
            else
            {
                // создаем новый элемент корзины
                var newCartItem = new CartItem
                {
                    CartID = userCart.ID,
                    ItemID = targetitemId,
                    Quantity = quantity,
                    Purchase = quantity * item.Price
                };
                cartItems.Add(newCartItem);
                // СОХРАНЕНИЕ В БД
                Core.Context.CartItem.Add(newCartItem);
            }

            // обновляем дату изменения корзины
            userCart.ChangeDate = DateTime.Now;

            // сохраняем
            Core.Context.SaveChanges();

            Console.WriteLine("Товар успешно добавлен в корзину.");
        }

        static void ShowCart(User user)
        {

            Cart userCart = carts.FirstOrDefault(cart => cart.UserID == user.ID);

            if (userCart != null)
            {
                Console.WriteLine("ТОВАРЫ В ВАШЕЙ КОРЗИНЕ");
                var itemsInCart = cartItems.Where(u => u.CartID == userCart.ID).ToList();

                foreach (var itemInCart in itemsInCart)
                {
                    
                    Item targetItem = items.FirstOrDefault(item => item.ID == itemInCart.ItemID);
                    Console.Write($"Товар: {targetItem.Name} ");
                    Console.WriteLine($"Количество: {itemInCart.Quantity}, Цена: {itemInCart.Purchase}");
                }

            }
            else
            {
                Console.WriteLine("Корзина пуста.");
                return;
            }

        }


        static User Login()
        {
            Console.WriteLine("Введите логин:");
            string login = Console.ReadLine();
            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            int passw;
            bool result = int.TryParse(password, out passw);

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Пароль не может быть пустым.");
                return null;
            }

            if (!result)
            {
                Console.WriteLine("Пароль МОЖЕТ состоять только из цифр!");
                return null;
            }


            var user = users.Find(u => u.Name == login && u.Password == passw);
            if (user != null)
            {
                Console.WriteLine("Успешный вход.");
                return user;
            }

            Console.WriteLine("Некорректный логин или пароль.");
            return null;
        }

        static void ShowItems()
        {
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.ID} Название товара: {item.Name} Цена: {item.Price:F2} руб.");
            }
        }


        static void Register()
        {
            Console.WriteLine("Введите логин: ");
            string login = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(login))
            {
                Console.WriteLine("Логин не может быть пустым.");
                return;
            }

            Console.WriteLine("Введите пароль(только цифры): ");
            string password = Console.ReadLine();

            int passw;
            bool result = int.TryParse(password, out passw);

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Пароль не может быть пустым.");
                return;
            }

            if (!result)
            {
                Console.WriteLine("Пароль МОЖЕТ состоять только из цифр!");
                return;
            }

            Console.WriteLine("Подтвердите пароль: ");
            string confirmPassword = Console.ReadLine();

            if (password != confirmPassword)
            {
                Console.WriteLine("Пароли не совпадают!");
                return;
            }

            if (users.Exists(u => u.Name == login))
            {
                Console.WriteLine("Пользователь с таким логином уже существует.");
                return;
            }

            User newUser = new User
            {
                Name = login,
                Password = passw
            };

            users.Add(newUser); // НУЖНО ЛИ ЭТО?? добавится ли сюда автоматически? или сл строка?

            Core.Context.User.Add(newUser); // добавление пользователя в таблицу в БД
            Core.Context.SaveChanges(); // сохранение изменений в БД

            Console.WriteLine("Регистрация прошла успешно.");
        }




    }
}
