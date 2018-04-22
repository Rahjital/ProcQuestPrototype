using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class PhonotacticsRule
    {
        public string[][] rules;

        public int weight = 1;
    }

    public class Morpheme
    {

    }

    public class MorphemeGenerator
    {
        public List<PhonotacticsRule> onsetRules = new List<PhonotacticsRule>();
        public List<PhonotacticsRule> nucleusRules = new List<PhonotacticsRule>();
        public List<PhonotacticsRule> codaRules = new List<PhonotacticsRule>();

        public string GenerateMorpheme()
        {
            if (onsetRules.Count == 0 || nucleusRules.Count == 0 || codaRules.Count == 0)
            {
                Log.LogMessage("MorphemeGenerator: Cannot generate morpheme, missing phonotactics rules!");
                return null;
            }

            /*PhonotacticsRule chosenOnsetRule = PRNG.SelectRandom(onsetRules);
            PhonotacticsRule chosenNucleusRule = PRNG.SelectRandom(nucleusRules);
            PhonotacticsRule chosenCodaRule = PRNG.SelectRandom(codaRules);*/

            PhonotacticsRule chosenOnsetRule = PRNG.SelectRandomByWeight(onsetRules, rule => rule.weight);
            PhonotacticsRule chosenNucleusRule = PRNG.SelectRandomByWeight(nucleusRules, rule => rule.weight);
            PhonotacticsRule chosenCodaRule = PRNG.SelectRandomByWeight(codaRules, rule => rule.weight);

            Log.LogMessage(String.Format("MorphemeGenerator: Generating a morpheme with {0} onset, {1} nucleus and {2} coda", 
                chosenOnsetRule.rules.Length, chosenNucleusRule.rules.Length, chosenCodaRule.rules.Length));

            int morphemeLength = chosenOnsetRule.rules.Length + chosenNucleusRule.rules.Length + chosenCodaRule.rules.Length;

            StringBuilder stringBuilder = new StringBuilder(morphemeLength);

            foreach (string[] variations in chosenOnsetRule.rules)
            {
                    string variation = PRNG.SelectRandom(variations);

                    stringBuilder.Append(variation);
            }

            foreach (string[] variations in chosenNucleusRule.rules)
            {
                    string variation = PRNG.SelectRandom(variations);

                    stringBuilder.Append(variation);
            }

            foreach (string[] variations in chosenCodaRule.rules)
            {
                    string variation = PRNG.SelectRandom(variations);

                    stringBuilder.Append(variation);
            }

            string result = stringBuilder.ToString();

            Log.LogMessage(String.Format("MorphemeGenerator: result: {0}", result));

            return result;
        }
    }
}
