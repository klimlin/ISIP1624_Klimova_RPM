using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP1624_Klimova_RPM.Classes
{

    public class RandomChoice
    {
        private static Random random = new Random();

        public bool GetRandomBoolean()
        {
            return random.Next(2) == 1;
        }

        public int GetRandomInteger012()
        {
            return random.Next(3);
        }

        public int GetRandomInt(int n)
        {
            return random.Next(n);
        }

        public int GetRandomInteger0123()
        {
            return random.Next(4);
        }

        public bool ChanceFortyPercent()
        {
            return random.NextDouble() <= 0.4;
        }

        public bool ChanceTwentyPercent()
        {
            return random.NextDouble() <= 0.2;
        }

        public double GetRandomDoubleInRange70()
        {
            return 0.7 + random.NextDouble() * (1.0 - 0.7);
        }
    }
}
