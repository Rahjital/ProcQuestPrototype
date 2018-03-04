using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public abstract class QuestNodeGoal
    {
        public QuestNode parentNode;

        public abstract string GetString();
        public abstract bool NewExpansionGoal(out QuestNodeGoal newQuestGoal);

        public virtual void CreateExpansionNode()
        {
            QuestNodeGoal newNodeGoal;

            if (NewExpansionGoal(out newNodeGoal))
            {
                // --
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
        }

        public virtual void LateInitialisation() {}
    }
}
