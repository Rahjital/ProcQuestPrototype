using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestNodeTargetPerson : QuestNodeTarget
    {
        public string name;

        public string disposition;
        public QuestInfoPosition position;

        public QuestNodeTargetPerson(string name, string disposition, QuestInfoPosition position)
        {
            this.name = name;
            this.disposition = disposition;
            this.position = position;
        }

        public QuestNodeTargetPerson(string name, string disposition, QuestNodeTargetLocation location)
        {
            this.name = name;
            this.disposition = disposition;
            this.position = new QuestInfoPosition(location);
        }

        public override string GetString()
        {
            return String.Format("PERSON {0}\n--- disposition: {1}\n--- position: {2}", name, disposition, position.GetString());
        }

        public override string GetStringShort()
        {
            return String.Format("PERSON {0} ({1}, {2})", name, disposition, position.GetString());
        }
    }
}
