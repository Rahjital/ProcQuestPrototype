using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestNodeGoalGet : QuestNodeGoal
    {
        public QuestNodeTarget target;

        public QuestNodeGoalGet(QuestNodeTarget target)
        {
            this.target = target;
        }

        public override string GetString()
        {
            return String.Format("GET\n{0}", target.GetString());
        }

        public override QuestNodeTarget GetSingleTarget()
        {
            return target;
        }
    }
}
