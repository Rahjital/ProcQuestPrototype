using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestNodeGoalGet : QuestNodeGoal
    {
        public QuestNodeTargetItem target;

        public QuestNodeGoalGet(QuestNodeTargetItem target)
        {
            this.target = target;
        }

        public override string GetString()
        {
            return String.Format("GET\n{0}", target.GetString());
        }

        public override bool NewExpansionGoal(out QuestNodeGoal newNodeGoal)
        {
            QuestNodeGoalExchange newExchangeGoal = new QuestNodeGoalExchange();

            newExchangeGoal.reward = target;

            if (target.position is QuestInfoPositionPossessed)
            {
                QuestInfoPositionPossessed ownerInfo = (QuestInfoPositionPossessed)target.position;

                newExchangeGoal.rewardGiver = ownerInfo.owner;
            }
            else if (target.position is QuestInfoPositionLocation)
            {
                QuestNodeTargetPerson newOwner = new QuestNodeTargetPerson(NameComposer.ComposeName(3, 9), "neutral", (QuestInfoPositionLocation)target.position);
            }
            else
            {
                throw new ArgumentException("QuestNodeGoalGet NewExpansionGoal - unknown QuestInfoPosition type");
            }

            newNodeGoal = newExchangeGoal;

            parentNode.goal = newNodeGoal;
            newNodeGoal.parentNode = parentNode;

            newNodeGoal.LateInitialisation();

            return false;
        }
    }
}
