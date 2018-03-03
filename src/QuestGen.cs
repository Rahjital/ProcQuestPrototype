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

    // -----
    class QuestGen
    {
        static void Main(string[] args)
        {
            List<QuestNode> questNodes = new List<QuestNode>();

            QuestInfoPositionLocation itemOwnerLocation = new QuestInfoPositionLocation(NameComposer.ComposeName(3, 9));

            QuestNodeTargetPerson itemOwner = new QuestNodeTargetPerson(NameComposer.ComposeName(3, 9), "hostile", itemOwnerLocation);

            QuestInfoPositionPossessed itemLocation = new QuestInfoPositionPossessed(itemOwner);
            QuestNodeTargetItem getItemTarget = new QuestNodeTargetItem("Dancing Dragon sword", itemLocation);
            QuestNodeGoalGet getItemGoal = new QuestNodeGoalGet(getItemTarget);
            QuestNode getItemNode = new QuestNode("startNode", getItemGoal);

            questNodes.Add(getItemNode);

            QuestInfoPositionLocation questGiverLocation = new QuestInfoPositionLocation(NameComposer.ComposeName(3, 9));
            questGiverLocation.knowledge = "known_always";

            QuestNodeTargetPerson questGiverPerson = new QuestNodeTargetPerson(NameComposer.ComposeName(3, 9), "friendly", questGiverLocation);
            QuestNodeGoalGive giveItemGoal = new QuestNodeGoalGive(getItemTarget, questGiverPerson);
            QuestNode deliverItemNode = new QuestNode("endNode", giveItemGoal);

            questNodes.Add(deliverItemNode);

            Quest quest = new Quest(questNodes);

            using (StreamWriter stream = new StreamWriter("questTest.txt"))
            {
                stream.AutoFlush = true;

                stream.WriteLine("Procedural quest test");
                stream.Write(quest.GetString());
            }
        }
    }
}
