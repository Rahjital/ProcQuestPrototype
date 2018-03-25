using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestNodeGoalGet : QuestNodeGoal
    {
        public QuestNodeTargetPossessable target;

        public QuestNodeGoalGet(QuestNodeTargetPossessable target) : base()
        {
            this.target = target;
        }

        public override string GetString()
        {
            return String.Format("GET\n{0}", target.GetString());
        }

        public override bool NewExpansionGoal(out QuestNodeGoal expansionGoal)
        {
            if (PRNG.Bool() && target.position.knowledge == "known")
            {
                QuestNodeTargetLocation informationHolderLocation = new QuestNodeTargetLocation(NameComposer.ComposeName(3, 9));
                QuestNodeTargetPerson informationHolder = new QuestNodeTargetPerson(NameComposer.ComposeName(3, 9), "neutral", informationHolderLocation);
                QuestInfoPosition informationPosition = new QuestInfoPosition(informationHolder);

                QuestNodeTargetInformation targetInfo = new QuestNodeTargetInformation(target.position, target, informationPosition);

                target.position.knowledge = "unknown";

                expansionGoal = new QuestNodeGoalGet(targetInfo);

                return true;
            }
            else
            {
                QuestNodeGoalExchange newExchangeGoal = new QuestNodeGoalExchange();

                newExchangeGoal.reward = target;

                if (target.position.target is QuestNodeTargetPerson)
                {
                    newExchangeGoal.rewardGiver = (QuestNodeTargetPerson)target.position.target;
                }
                else if (target.position.target is QuestNodeTargetLocation)
                {
                    string oldPositionKnowledge = target.position.knowledge;

                    newExchangeGoal.rewardGiver = new QuestNodeTargetPerson(NameComposer.ComposeName(3, 9), "neutral", (QuestNodeTargetLocation)target.position.target);
                    target.position = new QuestInfoPosition(newExchangeGoal.rewardGiver);
                    target.position.knowledge = oldPositionKnowledge;
                }
                else
                {
                    throw new ArgumentException("QuestNodeGoalGet NewExpansionGoal - unknown QuestInfoPosition type");
                }

                parentNode.goal = newExchangeGoal;
                newExchangeGoal.parentNode = parentNode;

                newExchangeGoal.LateInitialisation();

                expansionGoal = null;

                return false;
            }
        }
    }
}
