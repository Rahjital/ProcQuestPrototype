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

    public class QuestNodeGoalGive : QuestNodeGoal
    {
        public QuestNodeTarget target;
        public QuestNodeTarget receivee;

        public QuestNodeGoalGive(QuestNodeTarget target, QuestNodeTarget receivee)
        {
            this.target = target;
            this.receivee = receivee;
        }

        public override string GetString()
        {
            return String.Format("GIVE\n{0}\nTO\n{1}", target.GetString(), receivee.GetString());
        }

        public override QuestNodeTarget GetSingleTarget()
        {
            return PRNG.Bool() ? target : receivee;
        }
    }
}
