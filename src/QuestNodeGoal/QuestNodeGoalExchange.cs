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

        public QuestNodeGoalExchange(QuestNodeTarget reward, QuestNodeGoal condition, QuestNodeTargetPerson rewardGiver) : base()
        {
            this.reward = reward;
            this.condition = condition;
            this.rewardGiver = rewardGiver;
        }

        public QuestNodeGoalExchange() : base() { }

        public override void LateInitialisation()
        {
            if (condition == null)
            {
                Log.LogMessage("[QuestNodeGoalExchange]: Creating a new condition");

                QuestInfoPosition itemPosition;

                if (PRNG.Bool())
                {
                    QuestNodeTargetLocation itemOwnerLocation = new QuestNodeTargetLocation(NameComposer.ComposeName(3, 9));
                    QuestInfoPosition itemOwnerPosition = new QuestInfoPosition(itemOwnerLocation);
                    QuestNodeTargetPerson itemOwner = new QuestNodeTargetPerson(NameComposer.ComposeName(3, 9), "neutral", itemOwnerPosition);
                    itemPosition = new QuestInfoPosition(itemOwner);
                }
                else
                {
                    QuestNodeTargetLocation itemLocation = new QuestNodeTargetLocation(NameComposer.ComposeName(3, 9));
                    itemPosition = new QuestInfoPosition(itemLocation);
                }

                QuestNodeTargetItem conditionItem = new QuestNodeTargetItem("Artifact " + NameComposer.ComposeName(3, 9), itemPosition);

                condition = new QuestNodeGoalDeliver(conditionItem, rewardGiver);

                // Merge with the method in QuestNodeGoal later on, to avoid code duplication
                QuestNode newNode = new QuestNode(parentNode.quest.GetNextNodeName(), condition);

                ConnectNewPrecedingNode(newNode);
            }
        }

        public override string GetString()
        {
            return String.Format("COMPLETE GOAL OF NODE\n{0}\nTO RECEIVE\n{1}", condition.parentNode.nodeName, reward.GetString());
        }

        public override bool NewExpansionGoal(out QuestNodeGoal expansionGoal)
        {
            expansionGoal = null;
            return true;
        }
    }
}
