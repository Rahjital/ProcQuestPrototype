using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class QuestNode
    {
        public string nodeName;
        public QuestNodeGoal goal;

        public QuestNode(string nodeName, QuestNodeGoal goal)
        {
            this.nodeName = nodeName;
            this.goal = goal;
        }

        public string GetString()
        {
            return String.Format("= Node {0} =\n{1}", nodeName, goal.GetString());
        }

        public QuestNode GetNodeToInsertBefore()
        {
            QuestNodeTarget target = goal.GetSingleTarget();

            QuestNode newNode = new QuestNode("node", null);

            return newNode;
        }
    }
}
