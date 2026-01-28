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



        static void Main(string[] args)
        {
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.ID} Название товара: {item.Name} Цена: {item.Price:F2} руб.");
            }
        }
    }
}
