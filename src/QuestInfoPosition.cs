using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public abstract class QuestInfo
    {
        public string knowledge;

        public abstract string GetString();
    }

    public abstract class QuestInfoPosition : QuestInfo
    {
    }

    public class QuestInfoPositionLocation : QuestInfoPosition
    {
        public string name;

        public QuestInfoPositionLocation(string name)
        {
            this.name = name;

            knowledge = "known";
        }

        public override string GetString()
        {
            return String.Format("[{0}] LOCATION {1}", knowledge, name);
        }
    }

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
