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

        public static T SelectRandom<T>(T[] array)
        {
            return array[PRNG.Int(array.Length)];
        }

        public static T SelectRandom<T>(List<T> list)
        {
            return list[PRNG.Int(list.Count)];
        }

        public static T SelectRandomByWeight<T>(IEnumerable<T> enumerable, Func<T, int> weightSelector)
        {
            int totalWeight = 0;

            foreach (T element in enumerable)
            {
                totalWeight += weightSelector(element);
            }

            int currentWeight = 0;
            int targetWeight = (int)Math.Round(totalWeight * PRNG.Double());

            //Log.LogMessage(String.Format("SelectRandomByWeight: Selecting weight {0} from total of {1}", targetWeight, totalWeight));

            foreach (T element in enumerable)
            {
                currentWeight += weightSelector(element);

                if (currentWeight > targetWeight)
                {
                    return element;
                }
            }

            return enumerable.Last();
        }
    }
}
