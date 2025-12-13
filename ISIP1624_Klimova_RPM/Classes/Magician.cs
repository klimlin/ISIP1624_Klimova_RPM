using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{


    // Архимаг Шанс заморозки +10%. к значению обычного мага.
    // Сохраняет: шанс наложить заморозку (пропуск хода).
    public class Magician : Monster
    {
        private RandomChoice randoming = new RandomChoice();
        private const int maghp = 15;
        private const int magdamage = 15;
        private const int magdefencing = 8;
        public override string _name { get; set; }
        public override int _hp { get; set; }
        public override int _damage { get; set; }
        public override int _defencing { get; set; }

        public Magician()
        {
            _name = "Маг";
            _hp = maghp;
            _damage = magdamage;
            _defencing = magdefencing;
        }

        public Magician(string name) // должно приходить слово босс
        {
            _name = name + " Архимаг C++ (маг)";
            _hp = (int)(maghp * 1.8);
            _damage = (int)(magdamage * 1.6);
            _defencing = (int)(magdefencing * 1.1);
        }

        public override int Attack()
        {
            return _damage;
        }

        public override bool freezingSpell()
        {
            return randoming.ChanceTwentyPercent();
        }
    }
}
