using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestNodeGoalAmbush : QuestNodeGoal
    {
        public QuestNodeTarget ambusher;

        public QuestNodeGoalAmbush(QuestNodeTarget ambusher)
        {
            this.ambusher = ambusher;
        }

        public QuestNodeGoalAmbush()
        {
            QuestInfoPositionLocation ambusherLocation = new QuestInfoPositionLocation(NameComposer.ComposeName(3, 9));
            QuestNodeTargetPerson ambusher = new QuestNodeTargetPerson(NameComposer.ComposeName(3, 9), "hostile", ambusherLocation);

            this.ambusher = ambusher;
        }

        public override string GetString()
        {
            return String.Format("AMBUSH BY\n{0}", ambusher.GetString());
        }

        public override bool NewExpansionGoal(out QuestNodeGoal newNodeGoal)
        {
            newNodeGoal = new QuestNodeGoalAmbush();

            return true;
        }
    }
}
