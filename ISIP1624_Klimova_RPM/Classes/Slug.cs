using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{
    // уменьшает входящий в него урон на 2 единицы

    public class Slug : Monster
    {
        private RandomChoice randoming = new RandomChoice();
        private const int slughp = 10;
        private const int slugdamage = 5;
        private const int slugdefencing = 5;

        public override string _name { get; set; }
        public override int _hp { get; set; }
        public override int _damage { get; set; }
        public override int _defencing { get; set; }

        public Slug()
        {
            _name = "Слизень";
            _hp = slughp;
            _damage = slugdamage;
            _defencing = slugdefencing;
        }


        public override int Attack()
        {

            return _damage;
        }

        public override void GetDamage(int damage)
        {
            damage -= _defencing;
            damage -= 2;
            _hp -= damage;
        }

    }
}
