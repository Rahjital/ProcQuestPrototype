using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProceduralQuestTest
{
    // Todo List
    // - Name class, which concerns giving stuff names and making words up
    // - Tags
    // --- Everything, from names to items to people to locations, has certain tags
    // --- These tags govern how things are related - ie a castle may have "medieval", "war", "fortress"
    // --- By default they have a value of 1, but a decimal value can be specified as well (castle may have "housing 0.2")
    // --- In certain cases, value may also be negative (spaceship may have "medieval -1")
    // --- These values are then added together to choose the final thing randomly from the highest results
    // --- In case of multiple things chosen in a single search, the search may work differently based on the purpose
    // ----- ie if a sword has tags for power and dexterity, and the random name chooser gets "dragon" at first, the tag values 
    //       of "dragon" get subtracted from the wanted result; it would then choose an adjective based on the remaining search
    //       for dexterity and perhaps get "dance", which would finally result in "Dancing Dragon"
    // ----- however, if fantasy creatures are being chosen, then the criteria of the search should not change based on creatures
    //       that were chosen previously.
    // - tangible things (items, locations, perhaps even people) also need a "rarity" value.
    // --- a hammer or even a surgeon's scalpel might be "common", whereas adamantine or a miracle herb may be "exotic"
    // --- quests should only be given for uncommon or even rare items, unless the demand is urgent.

    // QuestNodeTarget types
    // - target - general, any kind of target
    // - person - a sapient creature of any kind - a human, a dragon, a golem, an magical floating cloud, ...
    // - possession - anything a person may carry on themselves, ie either item or knowledge
    // - item - a physical, tangible thing
    // - knowledge - knowledge of any sort - information on the location of someone or something, knowhow on how to do something, etc.

    // QuestNodeGoals
    // - GET possession FROM person
    // --- basically just get something from them for free (if friendly)
    // - EXCHANGE possession1 FOR possession2
    // - DESTROY target
    // - DELIVER possession

    // New Quest Node addition kinds
    // - add unaffiliated - 
    // - add related -
    // - replace self -

    // -----
    class QuestTest
    {
        static void Main(string[] args)
        {
            try
            {
                Log.LogMessage("Starting quest generation");

                Quest quest = new Quest();

                // TWO BASIC NODES STARTING HERE
                QuestNodeTargetLocation itemOwnerLocation = new QuestNodeTargetLocation(NameComposer.ComposeName(3, 9));
                QuestNodeTargetPerson itemOwner = new QuestNodeTargetPerson(NameComposer.ComposeName(3, 9), "neutral", itemOwnerLocation);

                QuestInfoPosition itemLocation = new QuestInfoPosition(itemOwner);
                QuestNodeTargetItem getItemTarget = new QuestNodeTargetItem("Dancing Dragon sword", itemLocation);
                QuestNodeGoalGet getItemGoal = new QuestNodeGoalGet(getItemTarget);
                QuestNode getItemNode = new QuestNode("startNode", getItemGoal);

                quest.AddNode(getItemNode);

                QuestNodeTargetLocation questGiverLocation = new QuestNodeTargetLocation(NameComposer.ComposeName(3, 9));
                QuestNodeTargetPerson questGiverPerson = new QuestNodeTargetPerson(NameComposer.ComposeName(3, 9), "friendly", questGiverLocation);
                questGiverPerson.position.knowledge = "known_always";

                QuestNodeGoalDeliver giveItemGoal = new QuestNodeGoalDeliver(getItemTarget, questGiverPerson);
                QuestNode deliverItemNode = new QuestNode("endNode", giveItemGoal);

                getItemNode.nextNode = deliverItemNode;
                deliverItemNode.previousNode = getItemNode;
                quest.AddNode(deliverItemNode);
                // TWO BASIC NODES ENDING HERE

                //deliverItemNode.CreateExpansionNode();
                getItemNode.CreateExpansionNode();

                for (int i = 0; i < 10; i++)
                {
                    quest.ExpandRandomNode();
                }

                using (StreamWriter stream = new StreamWriter("questTest.txt"))
                {
                    stream.WriteLine("Procedural quest test");
                    stream.Write(quest.GetString());
                }

                Log.LogMessage("Quest generation done");
                Log.LogMessage(String.Format("Quest nodes generated: {0}", quest.nodes.Count));

                // ===== Concept Loading =====
                ConceptLoader.LoadConcepts();

                // ===== Language Generation =====
                Language language = new Language();

                // { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "r", "s", "t", "w" }

                PhonotacticsRule onsetRule0 = new PhonotacticsRule();
                onsetRule0.rules = new string[][] { };
                onsetRule0.weight = 50;

                PhonotacticsRule onsetRule1 = new PhonotacticsRule();
                onsetRule1.rules = new string[][] {
                    new string[] { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "r", "s", "t", "w" }
                };
                onsetRule1.weight = 100;

                PhonotacticsRule onsetRule2_1 = new PhonotacticsRule();
                onsetRule2_1.rules = new string[][] {
                    new string[] { "s" },
                    new string[] { "p", "t", "k", "f", "m", "n" }
                };
                onsetRule2_1.weight = 15;

                PhonotacticsRule onsetRule2_2 = new PhonotacticsRule();
                onsetRule2_2.rules = new string[][] {
                    new string[] { "p", "t", "k", "b", "d", "g", "f", "th", "s", "sh", "h", "v", "m", "n", "l" },
                    new string[] { "l", "r", "j", "w", "ua" }
                };
                onsetRule2_2.weight = 15;

                PhonotacticsRule onsetRule3 = new PhonotacticsRule();
                onsetRule3.rules = new string[][] {
                    new string[] { "s" },
                    new string[] { "p", "t", "k" },
                    new string[] { "l", "r", "j", "w", "ua" }
                };

                language.morphemeGenerator.onsetRules.Add(onsetRule0);
                language.morphemeGenerator.onsetRules.Add(onsetRule1);
                language.morphemeGenerator.onsetRules.Add(onsetRule2_1);
                language.morphemeGenerator.onsetRules.Add(onsetRule2_2);
                language.morphemeGenerator.onsetRules.Add(onsetRule3);

                PhonotacticsRule nucleusRule1 = new PhonotacticsRule();
                nucleusRule1.rules = new string[][] {new string[] { "a", "e", "i", "o", "u"}};

                language.morphemeGenerator.nucleusRules.Add(nucleusRule1);

                PhonotacticsRule codaRule0 = new PhonotacticsRule();
                codaRule0.rules = new string[][] { };
                codaRule0.weight = 100;

                PhonotacticsRule codaRule1 = new PhonotacticsRule();
                codaRule1.rules = new string[][] {
                    new string[] { "b", "c", "d", "f", "g", "k", "l", "m", "n", "p", "s", "t" }
                };
                codaRule1.weight = 200;

                PhonotacticsRule codaRule2_1 = new PhonotacticsRule();
                codaRule2_1.rules = new string[][] {
                    new string[] { "m", "n", "l", "s" },
                    new string[] { "p", "k", "d", "f" }
                };
                codaRule2_1.weight = 25;

                PhonotacticsRule codaRule2_2 = new PhonotacticsRule();
                codaRule2_2.rules = new string[][] {
                    new string[] { "p", "k", "d", "f" },
                    new string[] { "s", "z", "t", "d", "th" }
                };
                codaRule2_2.weight = 25;

                PhonotacticsRule codaRule3_1 = new PhonotacticsRule();
                codaRule3_1.rules = new string[][] {
                    new string[] { "m", "n", "l", "s" },
                    new string[] { "p", "k", "d", "f" },
                    new string[] { "s", "z", "t", "d", "th" }
                };
                codaRule3_1.weight = 10;

                PhonotacticsRule codaRule3_2_1 = new PhonotacticsRule();
                codaRule3_2_1.rules = new string[][] {
                    new string[] { "p", "k", "f" },
                    new string[] { "th" },
                    new string[] { "s" }
                };
                codaRule3_2_1.weight = 5;

                PhonotacticsRule codaRule3_2_2 = new PhonotacticsRule();
                codaRule3_2_2.rules = new string[][] {
                    new string[] { "p", "k", "f" },
                    new string[] { "s" },
                    new string[] { "t" }
                };
                codaRule3_2_2.weight = 5;

                PhonotacticsRule codaRule4_1 = new PhonotacticsRule();
                codaRule4_1.rules = new string[][] {
                    new string[] { "l", "m" },
                    new string[] { "f", "p" },
                    new string[] { "t", "th" },
                    new string[] { "s" }
                };

                PhonotacticsRule codaRule4_2 = new PhonotacticsRule();
                codaRule4_2.rules = new string[][] {
                    new string[] { "k" },
                    new string[] { "s" },
                    new string[] { "t", "th" },
                    new string[] { "s" }
                };

                language.morphemeGenerator.codaRules.Add(codaRule0);
                language.morphemeGenerator.codaRules.Add(codaRule1);
                language.morphemeGenerator.codaRules.Add(codaRule2_1);
                language.morphemeGenerator.codaRules.Add(codaRule2_2);
                language.morphemeGenerator.codaRules.Add(codaRule3_1);
                language.morphemeGenerator.codaRules.Add(codaRule3_2_1);
                language.morphemeGenerator.codaRules.Add(codaRule3_2_2);
                language.morphemeGenerator.codaRules.Add(codaRule4_1);
                language.morphemeGenerator.codaRules.Add(codaRule4_2);

                using (StreamWriter stream = new StreamWriter("languageTest.txt"))
                {
                    stream.WriteLine("Morpheme test:");
                    stream.WriteLine();

                    for (int i = 0; i < 100; i++)
                    {
                        stream.WriteLine(language.morphemeGenerator.GenerateMorpheme());
                    }
                }
            }
            catch (Exception exception)
            {
                Log.LogMessage(exception.ToString());
            }
        }
    }
}
