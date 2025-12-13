using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{

    public class Player
    {
        private int _hp;
        private int _maxHP = 100;
        private Weapon _usingWeapon;
        private Armour _usingArmour;
        private bool freezed = false;

        public Player()
        {
            _hp = _maxHP;
            _usingArmour = new Armour("Обычные доспехи", 15);
            _usingWeapon = new Weapon("Обычное оружие", 20);
        }

        public void GetNewWeapon(Weapon newWeapon)
        {
            _usingWeapon = newWeapon;
        }

        public void GetNewArmour(Armour newArmour)
        {
            _usingArmour = newArmour;
        }

        public void printInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ИГРОК (Вы)");
            Console.WriteLine($"HP: {_hp}");
            _usingArmour.printInfo();
            _usingWeapon.printInfo();
            if (freezed) { Console.WriteLine("ВЫ ПРОПУСКАЕТЕ ХОД"); }
            Console.ResetColor();
        }

        public bool IsDead() => _hp <= 0;

        public bool isFreezed()
        {
            return freezed;
        }

        public void getFreezed()
        {
            freezed = true;
        }

        public void healFromFreezing()
        {
            freezed = false;
        }

        public void GetDamage(int damage)
        {
            damage -= _usingArmour._defencing;
            if (damage < 0) { damage = 0; }
            _hp -= damage;
            Console.WriteLine($"Получен урон: {damage}, текущее здоровье: {_hp}");
        }

        public void GetDamageAvoidingDefence(int damage)
        {
            _hp -= damage;
            Console.WriteLine($"Получен урон: {damage}, текущее здоровье: {_hp}");
        }

        public int Attack()
        {
            return _usingWeapon.attackDamage();
        }



        public void GetHealing()
        {
            _hp = _maxHP;
        }
    }
}
