using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{


    public class Armour
    {
        private string _name;
        public int _defencing { get; } // неизменяемое значение 

        public Armour(string name, int defencing)
        {
            _name = name;
            _defencing = defencing;
        }

        public void printInfo()
        {
            Console.WriteLine("ДОСПЕХИ");
            Console.WriteLine($"Название: {_name}");
            Console.WriteLine($"Защита: {_defencing}");
        }
    }
}
