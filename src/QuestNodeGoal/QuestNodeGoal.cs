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
        // returns a bool because some goals replace themselves rather than creating a new node, and we need to avoid creating a new node when that happens
        // returns false when new node should NOT be created, and true if we can continue on
        public abstract bool NewExpansionGoal(out QuestNodeGoal expansionGoal);

        public QuestNodeGoal()
        {
            Log.LogMessage(String.Format("Creating a goal of type {0}", this.GetType().Name));
        }

        public virtual bool CreateExpansionNode()
        {
            Log.LogMessage(String.Format("Expanding node {0}, goal type {1}", parentNode.nodeName, this.GetType().Name));

            // See if want a generic expansion node; if not, try to create one specific to this goal type, but if that fails, come back to the guaranteed types and ensure one will get selected
            QuestNodeGoal newNodeGoal = NewGenericExpansionGoal();

            if (newNodeGoal == null)
            {
                if (!NewExpansionGoal(out newNodeGoal))
                {
                    Log.LogMessage("Goal blocks creating a new node, aborting");
                    return true;
                }
            }

            // Try to guarantee a generic expansion goal will be created if a specific can't be found - DISABLED FOR NOW
            /*if (newNodeGoal == null)
            {
                newNodeGoal = NewGenericExpansionGoal(true);
            }*/

            if (newNodeGoal != null)
            {
                // --
                QuestNode newNode = new QuestNode(parentNode.quest.GetNextNodeName(), newNodeGoal);

                ConnectNewPrecedingNode(newNode);

                return true;
            }

            return false;
        }

        public virtual QuestNodeGoal NewGenericExpansionGoal(bool guaranteed = false)
        {
            if (guaranteed || PRNG.Double() < 0.1)
            {
                // Only return an Ambush goal if there's not one before us already, to prevent chaining them; also don't make ambush the first node
                if ((parentNode.previousNode != null) && !(parentNode.previousNode.goal is QuestNodeGoalAmbush))
                {
                    return new QuestNodeGoalAmbush();
                }
            }

            return null;
        }

        public void ConnectNewPrecedingNode(QuestNode newPrecedingNode)
        {
            if (parentNode.previousNode != null)
            {
                parentNode.previousNode.nextNode = newPrecedingNode;
                newPrecedingNode.previousNode = parentNode.previousNode;
            }
            else
            // No previous node means our node was the first, and thus we have to tell Quest there's a new first node
            {
                parentNode.quest.firstNode = newPrecedingNode;
            }

            parentNode.previousNode = newPrecedingNode;
            newPrecedingNode.nextNode = parentNode;

            parentNode.quest.AddNode(newPrecedingNode);

            newPrecedingNode.goal.LateInitialisation();
        }

        public virtual void LateInitialisation() {}
    }
}
