using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ProceduralQuestTest
{
    public static class ConceptLoader
    {
        public static List<string> ParseJsonFile(string filename)
        {
            int brackets = 0;
            string jsonFile = new StreamReader(filename).ReadToEnd();
            List<string> JsonItems = new List<string>();
            StringBuilder Json = new StringBuilder();

            bool hasNode = false;

            foreach (char c in jsonFile)
            {
                if (c == '{')
                {
                    brackets++;
                    hasNode = true;
                }
                else if (c == '}')
                {
                    brackets--;
                }

                if (hasNode && c != ' ' && c != '\n' && c != '\r' && c != '\t')
                {
                    Json.Append(c);

                    if (brackets == 0)
                    {
                        JsonItems.Add(Json.ToString());
                        Json = new StringBuilder();

                        hasNode = false;
                    }
                }
            }

            return JsonItems;
        }

        public static void LoadConcepts()
        {
            Dictionary<string, int> categories = new Dictionary<string, int>();

            foreach (string filename in Directory.EnumerateFiles("data/concepts", "*.json", SearchOption.AllDirectories))
            {
                List<string> jsonNodes = ParseJsonFile(filename);

                Log.LogMessage(String.Format("Parsing {0} nodes from file {1}", jsonNodes.Count, filename));

                for (int i = 0; i < jsonNodes.Count; i++)
                {
                    Concept concept = JsonConvert.DeserializeObject<Concept>(jsonNodes[i]);

                    foreach (string category in concept.categories)
                    {
                        if (categories.ContainsKey(category))
                        {
                            categories[category] += 1;
                        }
                        else
                        {
                            categories[category] = 1;
                        }
                    }
                }
            }

            using (StreamWriter stream = new StreamWriter("conceptLoader.txt"))
            {
                stream.WriteLine("Categories:");
                stream.WriteLine();

                foreach (KeyValuePair<string, int> categoryKV in categories)
                {
                    stream.WriteLine(String.Format("{0}: {1}", categoryKV.Key, categoryKV.Value));
                }
            }
        }
    }
}
