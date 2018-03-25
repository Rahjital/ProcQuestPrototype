using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestNodeTargetLocation : QuestNodeTarget
    {
        public string name;

        public QuestNodeTargetLocation(string name)
        {
            this.name = name;
        }

        public override string GetString()
        {
            return String.Format("LOCATION {0}", name);
        }

        public override string GetStringShort()
        {
            return String.Format("LOCATION {0}", name);
        }
    }
}
