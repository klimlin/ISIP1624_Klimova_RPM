
using NagievShop2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagievShop2
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

        // правильно ли сохраняю данные? можно ли как-то улучшить? сохраняею в 3 строки
        // пароль только цифры (как поменять формат, если меняю формат в БД?) вылетали ошибки
        // формат даты - сохраняет только дату саму, без времени
        // например, если забыла создать везде primary key или внесла другие изменения в БД, как их синхронизировать с Visual studio


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
                Console.WriteLine("4. Заказать 1 товар из списка товаров");
                Console.WriteLine("5. Заказать 1 товар из корзины");
                Console.WriteLine("6. Заказать все товары в корзине и очистить корзину");
                Console.WriteLine("7. Мои заказы");
                Console.WriteLine("8. Выход");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ShowItems(); break;
                    case "2": AddItemToCart(user); break;
                    case "3": ShowCart(user); break;
                    case "4": OrderSingleItem(user); break;
                    case "5": OrderOneItemFromCart(user); break;
                    case "6": OrderAllItemsInCart(user); break;
                    case "7": ShowUserOrders(user); break;
                    case "8": exit = true; break;
                    default: Console.WriteLine("Некорректный ввод. Попробуйте снова."); break;
                }
            }
        }



        static void OrderOneItemFromCart(User user)
        {

            var userCart = carts.FirstOrDefault(c => c.UserID == user.ID);
            if (userCart == null)
            {
                Console.WriteLine("У вас нет товаров в корзине.");
                return;
            }

            // все товары в корзине
            var itemsInCart = cartItems.Where(u => u.CartID == userCart.ID).ToList();

            if (itemsInCart.Count == 0)
            {
                Console.WriteLine("Ваша корзина пуста.");
                return;
            }

            Console.WriteLine("Товары в корзине:");
            foreach (var itemInCart in itemsInCart)
            {
                var item = items.FirstOrDefault(i => i.ID == itemInCart.ItemID);
                if (item != null)
                {
                    Console.WriteLine($"ID: {item.ID} | Название: {item.Name} | Количество: {itemInCart.Quantity} | Цена за единицу: {item.Price:F2} руб.");
                }
            }

            Console.WriteLine("Введите ID товара для покупки:");
            string itemIdInput = Console.ReadLine();
            if (!int.TryParse(itemIdInput, out int targetitemId))
            {
                Console.WriteLine("Некорректный ID товара.");
                return;
            }

            // находим товар в корзине
            var cartItem = itemsInCart.FirstOrDefault(u => u.ItemID == targetitemId);
            if (cartItem == null)
            {
                Console.WriteLine("Этот товар отсутствует в вашей корзине.");
                return;
            }

            // находит товар в общей коллекции и фиксируем (фиксируем цену за 1 товар)
            var itemToBuy = items.FirstOrDefault(i => i.ID == targetitemId);
            if (itemToBuy == null)
            {
                Console.WriteLine("Товар не найден в базе.");
                return;
            }

            // запрос количества
            Console.WriteLine($"Введите количество товара для покупки (доступно в корзине: {cartItem.Quantity}):");
            string quantityInput = Console.ReadLine();
            if (!int.TryParse(quantityInput, out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Некорректное количество.");
                return;
            }

            if (quantity > cartItem.Quantity)
            {
                Console.WriteLine("Запрошенное количество превышает доступное в корзине.");
                return;
            }

            // оформление заказа
            decimal purchaseSum = quantity * itemToBuy.Price;

            // pickuppointID
            int adressId = 0;

            Console.WriteLine("Доступные пункты выдачи:");
            foreach (var point in pickUpPoints)
            {
                Console.WriteLine($"ID: {point.ID} Адрес: {point.Adress}");
            }

            Console.WriteLine("Введите ID пункта выдачи:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out adressId))
            {
                // Ищем по ID

                if (pickUpPoints.FirstOrDefault(p => p.ID == adressId) == null)
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
                Purchase = purchaseSum
            };
            orders.Add(newOrder);
            Core.Context.Order.Add(newOrder);
            Core.Context.SaveChanges();

            // создаем запись для товара в таблице ItemOrder
            ItemOrder itemOrder = new ItemOrder
            {
                OrderID = newOrder.ID,
                ItemID = itemToBuy.ID,
                Quantity = quantity,
                Purchase = itemToBuy.Price
            };
            itemOrders.Add(itemOrder);
            Core.Context.ItemOrder.Add(itemOrder);
            Core.Context.SaveChanges();

            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"Общая стоимость покупки: {purchaseSum:F2} руб.");
            Console.WriteLine(new string('-', 40));

            // обновляем количество товара в корзине
            if (quantity == cartItem.Quantity)
            {
                // удаляем товар из корзины
                cartItems.Remove(cartItem);
                Core.Context.CartItem.Remove(cartItem);
            }
            else
            {
                // уменьшаем количество!!!
                cartItem.Quantity -= quantity;
                cartItem.Purchase = cartItem.Quantity * itemToBuy.Price;
                Core.Context.SaveChanges();
                
            }

            // Обновляем дату изменения корзины
            userCart.ChangeDate = DateTime.Now;
            Core.Context.SaveChanges();

            Console.WriteLine("Товар успешно приобретен, корзина обновлена.");
        }

        // верны ли сохранения выше?

        static void OrderAllItemsInCart(User user)
        {
            // ищем корзину пользователя
            var userCart = carts.FirstOrDefault(c => c.UserID == user.ID);
            if (userCart == null)
            {
                Console.WriteLine("У вас нет товаров в корзине.");
                return;
            }

            // ищем все товары в корзине
            var itemsInCart = cartItems.Where(u => u.CartID == userCart.ID).ToList();

            if (itemsInCart.Count == 0)
            {
                Console.WriteLine("Ваша корзина пуста.");
                return;
            }

            // pickuppointID
            int adressId = 0;

            Console.WriteLine("Доступные пункты выдачи:");
            foreach (var point in pickUpPoints)
            {
                Console.WriteLine($"ID: {point.ID} Адрес: {point.Adress}");
            }

            Console.WriteLine("Введите ID пункта выдачи:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out adressId))
            {
                // Ищем по ID

                if (pickUpPoints.FirstOrDefault(p => p.ID == adressId) == null)
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

            Order newOrder = new Order
            {
                UserID = user.ID,
                Date = DateTime.Now,
                PickUpPointID = adressId,
                Purchase = itemsInCart.Sum(u => u.Purchase)
            };

            orders.Add(newOrder); // сохраняем коллекцию
            Core.Context.Order.Add(newOrder);
            Core.Context.SaveChanges();
            decimal finalPurchase = 0;
            // создаем записи для каждого товара в заказе
            foreach (var cartItem in itemsInCart)
            {
                ItemOrder itemOrder = new ItemOrder
                {
                    OrderID = newOrder.ID,
                    ItemID = cartItem.ItemID,
                    Quantity = cartItem.Quantity,
                    Purchase = cartItem.Purchase / cartItem.Quantity
                };

                finalPurchase += cartItem.Purchase;
                itemOrders.Add(itemOrder); // сохраняем в коллекцию
                Core.Context.ItemOrder.Add(itemOrder);
            }

            // сохраняем все изменения
            Core.Context.SaveChanges();

            // ОЧИЩАЕМ корзину

            foreach (var cartItem in itemsInCart)
            {
                cartItems.Remove(cartItem);
                Core.Context.CartItem.Remove(cartItem);
            }
            Core.Context.SaveChanges();

            Console.WriteLine("Ваш заказ успешно оформлен и корзина очищена.");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"ИТОГОВАЯ СТОИМОСТЬ: {finalPurchase}");
            Console.WriteLine(new string('-', 40));
        }

        static void ShowUserOrders(User user)
        {
            //  сортируем по дате 
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

                var pickUpPoint = pickUpPoints.FirstOrDefault(p => p.ID == order.PickUpPointID);
                Console.WriteLine($"Пункт выдачи: {pickUpPoint.Adress}");
                Console.WriteLine($"Общая сумма: {order.Purchase:F2} руб.");

                // Получаем связанные товары
                var itemsInOrder = itemOrders.Where(u => u.OrderID == order.ID).ToList();

                Console.WriteLine("Товары в заказе: ");

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

            Console.WriteLine("Доступные пункты выдачи:");
            foreach (var point in pickUpPoints)
            {
                Console.WriteLine($"ID: {point.ID} Адрес: {point.Adress}");
            }

            Console.WriteLine("Введите ID пункта выдачи:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out adressId))
            {
                // Ищем по ID
                if (pickUpPoints.FirstOrDefault(p => p.ID == adressId) == null)
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
            orders.Add(newOrder); // сохраняем
            Core.Context.Order.Add(newOrder);
            Core.Context.SaveChanges();

            // связываем товар с заказом через таблицу ItemOrder 
            ItemOrder itemOrder = new ItemOrder
            {
                OrderID = newOrder.ID,
                ItemID = itemToOrder.ID,
                Quantity = quantity,
                Purchase = quantity * itemToOrder.Price
            };
            itemOrders.Add(itemOrder);
            Core.Context.ItemOrder.Add(itemOrder);
            Core.Context.SaveChanges();

            Console.WriteLine($"Заказ на товар '{itemToOrder.Name}' оформлен успешно!");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"ИТОГОВАЯ СТОИМОСТЬ: {itemOrder.Purchase}");
            Console.WriteLine(new string('-', 40));
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
                // сохраняем
                carts.Add(userCart);
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

            // сохраняем в БД cartitem - это View???
            Core.Context.SaveChanges();

            Console.WriteLine("Товар успешно добавлен в корзину.");
        }

        static void ShowCart(User user)
        {

            Cart userCart = carts.FirstOrDefault(cart => cart.UserID == user.ID);

            if (userCart != null)
            {
                var itemsInCart = cartItems.Where(u => u.CartID == userCart.ID).ToList();
                Console.WriteLine("ТОВАРЫ В ВАШЕЙ КОРЗИНЕ");

                decimal finalPurchase = 0;

                if(itemsInCart.Count() != 0)
                {
                    foreach (var itemInCart in itemsInCart)
                    {
                        finalPurchase += itemInCart.Purchase;
                        Item targetItem = items.FirstOrDefault(item => item.ID == itemInCart.ItemID);
                        Console.Write($"Товар: {targetItem.Name} ");
                        Console.WriteLine($"Количество: {itemInCart.Quantity}, Стоимость: {itemInCart.Purchase}");
                    }

                    Console.WriteLine(new string('-', 40));
                    Console.WriteLine($"ИТОГОВАЯ СТОИМОСТЬ: {finalPurchase}");
                    Console.WriteLine(new string('-', 40));

                } else
                {
                    Console.WriteLine("В вашей корзине нет товаров.");
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

            users.Add(newUser); // обновляем коллекцию
            Core.Context.User.Add(newUser); // добавление пользователя в таблицу в БД
            Core.Context.SaveChanges(); // сохранение изменений в БД

            Console.WriteLine("Регистрация прошла успешно.");
        }




    }
}
