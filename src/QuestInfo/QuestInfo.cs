using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public abstract class QuestInfo
    {
        public string knowledge;

        public abstract string GetString();
        public abstract string GetStringShort();
    }
}
