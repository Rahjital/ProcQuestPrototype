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

        public QuestNodeGoalDeliver(QuestNodeTargetItem target, QuestNodeTargetPerson receivee)
        {
            this.target = target;
            this.receivee = receivee;
        }

        public QuestNodeGoalDeliver()
        {
        }

        public override void LateInitialisation()
        {
            QuestNodeGoalGet newNodeGoal = new QuestNodeGoalGet(target);

            // Merge with the method in QuestNodeGoal later on, to avoid code duplication
            QuestNode newNode = new QuestNode(parentNode.quest.GetNextNodeName(), newNodeGoal);

            // --
            if (parentNode.previousNode != null)
            {
                parentNode.previousNode.nextNode = newNode;
                newNode.previousNode = parentNode.previousNode;
            }
            else
            // No previous node means our node was the first, and thus we have to tell Quest there's a new first node
            {
                parentNode.quest.firstNode = newNode;
            }

            parentNode.previousNode = newNode;
            newNode.nextNode = parentNode;

            parentNode.quest.AddNode(newNode);

            newNode.goal.LateInitialisation();
        }

        public override string GetString()
        {
            return String.Format("DELIVER\n{0}\nTO\n{1}", target.GetString(), receivee.GetString());
        }

        public override bool NewExpansionGoal(out QuestNodeGoal newNodeGoal)
        {
            newNodeGoal = new QuestNodeGoalAmbush();

            return true;
        }
    }
}
