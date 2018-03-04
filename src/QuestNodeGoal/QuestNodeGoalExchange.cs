using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestNodeGoalExchange : QuestNodeGoal
    {
        public QuestNodeTargetPerson rewardGiver;
        public QuestNodeTarget reward;
        public QuestNodeGoal condition;

        public QuestInfoPositionLocation location;

        public QuestNodeGoalExchange(QuestNodeTarget reward, QuestNodeGoal condition, QuestNodeTargetPerson rewardGiver)
        {
            this.reward = reward;
            this.condition = condition;
            this.rewardGiver = rewardGiver;
        }

        public QuestNodeGoalExchange() {}

        public override void LateInitialisation()
        {
            if (condition == null)
            {
                QuestInfoPosition itemLocation;

                if (PRNG.Bool())
                {
                    QuestInfoPositionLocation itemOwnerLocation = new QuestInfoPositionLocation(NameComposer.ComposeName(3, 9));
                    QuestNodeTargetPerson itemOwner = new QuestNodeTargetPerson(NameComposer.ComposeName(3, 9), "neutral", itemOwnerLocation);
                    itemLocation = new QuestInfoPositionPossessed(itemOwner);
                }
                else
                {
                    itemLocation = new QuestInfoPositionLocation(NameComposer.ComposeName(3, 9));
                }

                QuestNodeTargetItem conditionItem = new QuestNodeTargetItem("Artifact " + NameComposer.ComposeName(3, 9), itemLocation);

                condition = new QuestNodeGoalDeliver(conditionItem, rewardGiver);

                // Merge with the method in QuestNodeGoal later on, to avoid code duplication
                QuestNode newNode = new QuestNode(parentNode.quest.GetNextNodeName(), condition);

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

        public override string GetString()
        {
            return String.Format("COMPLETE GOAL OF NODE\n{0}\nTO RECEIVE\n{1}", condition.parentNode.nodeName, reward.GetString());
        }

        public override bool NewExpansionGoal(out QuestNodeGoal newNodeGoal)
        {
            newNodeGoal = new QuestNodeGoalAmbush();

            return true;
        }
    }
}
