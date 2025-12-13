using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{

    public class Weapon
    {
        private string _name;
        private int _damage;

        public Weapon(string name, int damage)
        {
            _name = name;
            _damage = damage;
        }

        public int attackDamage()
        {
            return _damage;
        }

        public void printInfo()
        {
            Console.WriteLine("ОРУЖИЕ");
            Console.WriteLine($"Название: {_name}");
            Console.WriteLine($"Урон: {_damage}");
        }
    }
}
