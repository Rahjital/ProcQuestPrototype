using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestNodeGoalDeliver : QuestNodeGoal
    {
        public QuestNodeTargetItem target;
        public QuestNodeTargetPerson receivee;

        public QuestNodeGoalDeliver(QuestNodeTargetItem target, QuestNodeTargetPerson receivee) : base()
        {
            this.target = target;
            this.receivee = receivee;
        }

        public QuestNodeGoalDeliver() : base() { }

        public override void LateInitialisation()
        {
            QuestNodeGoalGet newNodeGoal = new QuestNodeGoalGet(target);

            // Merge with the method in QuestNodeGoal later on, to avoid code duplication
            QuestNode newNode = new QuestNode(parentNode.quest.GetNextNodeName(), newNodeGoal);

            ConnectNewPrecedingNode(newNode);
        }

        public override string GetString()
        {
            return String.Format("DELIVER\n{0}\nTO\n{1}", target.GetString(), receivee.GetString());
        }

        public override bool NewExpansionGoal(out QuestNodeGoal expansionGoal)
        {
            expansionGoal = null;
            return true;
        }
    }
}
