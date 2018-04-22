using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    // NUMBER
    // - Singular
    // - Plural
    // ?- Pluralia Tantum
    // ?- Dual
    // ?- Paucal

    // CASE
    // - Nominative
    // ?- Genitive
    // ?- Dative
    // ?- Accusative
    // ?- Vocative
    // ?- Locative
    // ?- Instrumental
    // ?- Ablative

    // GENDER
    // ?- Masculine
    // ?- Feminine
    // ?- Neuter
    // --- Variation for each: Animate/inanimate
    // --- Masculine-Feminine can be joined into Common instead

    public class Language
    {
        public MorphemeGenerator morphemeGenerator = new MorphemeGenerator();
    }
}
