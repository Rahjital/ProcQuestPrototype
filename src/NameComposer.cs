using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public static class NameComposer
    {
        private static string[] vowels = { "a", "e", "i", "o", "u", "ae" };
        private static string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "r", "s", "t", "w" };

        public static string ComposeName(int minLength, int maxLength)
        {
            string result = "";

            int wordLength = PRNG.Int(minLength, maxLength + 1);

            bool wantsVowel = PRNG.Bool();

            for (int i = 0; i < wordLength; i++)
            {
                if (wantsVowel)
                {
                    result += vowels[PRNG.Int(vowels.Length)];
                }
                else
                {
                    result += consonants[PRNG.Int(consonants.Length)];
                }

                wantsVowel = !wantsVowel;
            }

            result = char.ToUpper(result[0]) + result.Substring(1);

            return result;
        }
    }
}
