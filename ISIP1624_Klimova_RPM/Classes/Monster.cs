using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{

    abstract public class Monster
    {
        public abstract string _name { get; set; }
        public abstract int _hp { get; set; }
        public abstract int _damage { get; set; }
        public abstract int _defencing { get; set; }

        public bool IsDead() => _hp <= 0; // return лямда выражение

        public void GetDamage(int damage)
        {
            damage -= _defencing;
            _hp -= damage;
        }

        public void printInfo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("МОНСТР");
            Console.WriteLine($"Раса: {_name}");
            Console.WriteLine($"Здоровье: {_hp}");
            Console.WriteLine($"Урон: {_damage}");
            Console.WriteLine($"Защита: {_defencing}");
            Console.ResetColor();
        }

        public abstract int Attack();

        public virtual bool avoidingDefence()
        {
            return false;
        }

        public virtual bool freezingSpell()
        {
            return false;
        }
    }
}
