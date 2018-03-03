using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    public class Quest
    {
        List<QuestNode> nodes;

        public Quest(List<QuestNode> initialNodes)
        {
            this.nodes = initialNodes;
        }

        public string GetString()
        {
            string result = "";

            for (int i = 0; i < nodes.Count; i++)
            {
                result += "\n" + nodes[i].GetString();

                if (i < nodes.Count - 1)
                {
                    result += "\n";
                }
            }

            return result;
        }
    }
}
