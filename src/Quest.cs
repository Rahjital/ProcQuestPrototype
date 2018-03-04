using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class Quest
    {
        public List<QuestNode> nodes;
        public QuestNode firstNode;

        public Quest(List<QuestNode> initialNodes)
        {
            this.nodes = initialNodes;
        }

        public Quest() {
            this.nodes = new List<QuestNode>();
        }

        public string GetString()
        {
            string result = "";

            /*for (int i = 0; i < nodes.Count; i++)
            {
                result += "\n" + nodes[i].GetString();

                if (i < nodes.Count - 1)
                {
                    result += "\n";
                }
            }*/

            QuestNode currentNode = firstNode;

            while (currentNode != null)
            {
                result += "\n" + currentNode.GetString() + "\n";

                currentNode = currentNode.nextNode;
            }

            return result;
        }

        public void AddNode(QuestNode node)
        {
            nodes.Add(node);
            node.quest = this;

            if (firstNode == null)
            {
                firstNode = node;
            }
        }

        public string GetNextNodeName()
        {
            return "node" + (nodes.Count + 1);
        }
    }
}
