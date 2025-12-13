using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{

    //Сохраняет: полностью игнорирует защиту игрока.
    public class Skeleton : Monster
    {
        private const int skeletonhp = 15;
        private const int skeletondamage = 12;
        private const int skeletondefencing = 10;
        public override string _name { get; set; }
        public override int _hp { get; set; }
        public override int _damage { get; set; }
        public override int _defencing { get; set; }

        public Skeleton()
        {
            _name = "Скелет";
            _hp = skeletonhp;
            _damage = skeletondamage;
            _defencing = skeletondefencing;
        }

        public Skeleton(string name) // должно приходить слово босс
        {
            _name = name + " Ковальский (скелет)";
            _hp = (int)(skeletondamage * 2.5);
            _damage = (int)(skeletondamage * 1.3);
            _defencing = (int)(skeletondefencing * 1.4);
        }

        public override int Attack()
        {
            return _damage;
        }

        public override bool avoidingDefence()
        {
            return true;
        }
    }
}
