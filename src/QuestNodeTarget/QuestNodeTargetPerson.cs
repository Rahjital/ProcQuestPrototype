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
        public QuestInfoPositionLocation positionLocation;

        public QuestNodeTargetPerson(string name, string disposition, QuestInfoPositionLocation location)
        {
            this.name = name;
            this.disposition = disposition;
            this.positionLocation = location;
        }

        public override string GetString()
        {
            return String.Format("PERSON {0}\n--- disposition: {1}\n--- position: {2}", name, disposition, positionLocation.GetString());
        }

        public override string GetStringShort()
        {
            return String.Format("PERSON {0} ({1}, {2})", name, disposition, positionLocation.GetString());
        }

        public override QuestNodeGoal GetExtenstionGoal()
        {
            throw new NotImplementedException();
        }
    }
}
