using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{

    // ВВГ Шанс крита +10%. к значению обычного гоблина.
    // Сохраняет: шанс критического удара.
    // критический удар х2

    public class Goblin : Monster
    {
        private RandomChoice randoming = new RandomChoice();
        private const int goblinhp = 15;
        private const int goblindamage = 10;
        private const int goblindefencing = 5;

        public override string _name { get; set; }
        public override int _hp { get; set; }
        public override int _damage { get; set; }
        public override int _defencing { get; set; }

        public Goblin()
        {
            _name = "Гоблин";
            _hp = goblinhp;
            _damage = goblindamage;
            _defencing = goblindefencing;
        }

        public Goblin(string name) // должно приходить слово босс
        {
            _name = name + " ВВГ (гоблин)";
            _hp = goblinhp * 2;
            _damage = (int)(goblindamage * 1.5);
            _defencing = (int)(goblindefencing * 1.2);
        }

        public override int Attack()
        {
            bool criricalStrike = randoming.ChanceTwentyPercent();
            if (criricalStrike)
            {
                _damage *= 2;
            }

            return _damage;
        }

    }

}
