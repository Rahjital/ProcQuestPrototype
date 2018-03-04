using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestInfoPositionPossessed : QuestInfoPosition
    {
        public QuestNodeTargetPerson owner;

        public QuestInfoPositionPossessed(QuestNodeTargetPerson owner)
        {
            this.owner = owner;

            knowledge = "known";
        }

        public override string GetString()
        {
            return String.Format("[{0}] POSSESSED BY {1}", knowledge, owner.GetStringShort());
        }
    }
}
