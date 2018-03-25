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

        public QuestNode previousNode;
        public QuestNode nextNode;

        public Quest quest;

        public QuestNode(string nodeName, QuestNodeGoal goal)
        {
            this.nodeName = nodeName;
            this.goal = goal;
            this.goal.parentNode = this;
        }

        public QuestNode(string nodeName)
        {
            this.nodeName = nodeName;
        }

        public string GetString()
        {
            return String.Format("= Node {0} =\n{1}", nodeName, goal.GetString());
        }

        public bool CreateExpansionNode()
        {
            return goal.CreateExpansionNode();
        }
    }
}
