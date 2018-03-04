using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
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
}
