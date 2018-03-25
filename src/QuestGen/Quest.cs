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
        public List<QuestNode> expandableNodes;
        public QuestNode firstNode;

        public Quest() {
            this.nodes = new List<QuestNode>();
            this.expandableNodes = new List<QuestNode>();
        }

        public string GetString()
        {
            string result = "";

            QuestNode currentNode = firstNode;

            while (currentNode != null)
            {
                result += "\n" + currentNode.GetString() + "\n";

                currentNode = currentNode.nextNode;
            }

            return result;
        }

        public bool ExpandRandomNode()
        {
            while (expandableNodes.Count > 0)
            {
                int selectedNode = PRNG.Int(expandableNodes.Count);

                if (expandableNodes[selectedNode].CreateExpansionNode())
                {
                    return true;
                }
                else
                {
                    expandableNodes.RemoveAt(selectedNode);
                    Log.LogMessage("Node cannot be expanded anymore, removing and trying again");
                }
            }

            Log.LogMessage("WARNING - Quest can no longer be expanded!");

            return false;
        }

        public void AddNode(QuestNode node)
        {
            nodes.Add(node);
            expandableNodes.Add(node);

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
