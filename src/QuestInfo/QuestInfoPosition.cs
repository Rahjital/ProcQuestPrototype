using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestInfoPosition : QuestInfo
    {
        public QuestNodeTarget target;

        public QuestInfoPosition(QuestNodeTarget target)
        {
            this.target = target;

            knowledge = "known";
        }

        public override string GetString()
        {
            if (target is QuestNodeTargetPerson)
            {
                return String.Format("[{0}] POSSESSED BY {1}", knowledge, target.GetStringShort());
            }

            if (target is QuestNodeTargetLocation)
            {
                return String.Format("[{0}] AT {1}", knowledge, target.GetStringShort());
            }

            return String.Format("[{0}] AT TARGET {1}", knowledge, target.GetStringShort());
        }

        public override string GetStringShort()
        {
            return "POSITION";
        }
    }
}
