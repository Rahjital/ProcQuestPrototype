using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public static class PRNG
    {
        private static Random random = new Random();

        public static double Double()
        {
            return random.NextDouble();
        }

        public static int Int(int maximum)
        {
            return random.Next(maximum);
        }

        public static int Int(int minimum, int maximum)
        {
            return random.Next(minimum, maximum);
        }

        public static bool Bool()
        {
            return random.NextDouble() >= 0.5;
        }
    }
}
