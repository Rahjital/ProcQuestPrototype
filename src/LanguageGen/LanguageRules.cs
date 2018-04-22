using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public enum LanguageRuleType
    {
        MORPHEHE_PREFIX,
        MORPHEME_AFFIX,
        PREPOSITION
    }

    public class GrammaticalCategory
    {
        string categoryName;

        List<string> variations;
    }

    public class LanguageRule
    {
        string ruleName;

        LanguageRuleType ruleType;

        List<GrammaticalCategory> categories;
    }

    public class LanguageRules
    {

    }
}
