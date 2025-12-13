using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<CarParts> carParts = Core.Context.CarParts.ToList();
            foreach (CarParts carPart in carParts)
            {
                Console.WriteLine(carPart.Name);
            }
        }
    }

}
