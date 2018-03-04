using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public abstract class QuestNodeGoal
    {
        public abstract string GetString();
        public abstract QuestNodeTarget GetSingleTarget();
    }
}
