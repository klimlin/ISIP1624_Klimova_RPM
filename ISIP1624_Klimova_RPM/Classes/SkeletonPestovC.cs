using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{

    // Сохраняет: полностью игнорирует защиту игрока.
    // Шанс заморозки +15%. к значению обычного мага.
    public class SkeletonPestovC : Skeleton
    {
        public override string _name { get; set; }
        public override int _hp { get; set; }
        public override int _damage { get; set; }
        public override int _defencing { get; set; }
        private RandomChoice randoming = new RandomChoice();

        public SkeletonPestovC()
        {
            _name = "БОСС: Пестов С-- (скелет)";
            _hp = 35;
            _damage = 27;
            _defencing = 4;
        }

        public override int Attack()
        {
            return _damage;
        }

        public override bool avoidingDefence()
        {
            return true;
        }

        public override bool freezingSpell()
        {
            return randoming.ChanceFortyPercent();
        }
    }
}
