using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestNodeTargetItem : QuestNodeTarget
    {
        public string name;
        public QuestInfoPosition position;

        public QuestNodeTargetItem(string name, QuestInfoPosition position)
        {
            this.name = name;
            this.position = position;
        }

        public override string GetString()
        {
            return String.Format("ITEM {0}\n--- position: {1}", name, position.GetString());
        }

        public override string GetStringShort()
        {
            return String.Format("ITEM {0} ({1})", name, position.GetString());
        }

        public override QuestNodeGoal GetExtenstionGoal()
        {
            throw new NotImplementedException();
        }
    }
}
