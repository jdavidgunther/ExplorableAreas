using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace InClassAssignmentWeek7
{
    public class NPC
    {
        public string Name { get; set; }

        public List<Item> Inventory = new List<Item>();

        public void TalkAndShowInventory()
        {
            WriteLine($"I have these very special items that you can trade for a low price because you're the main chara- oh I mean, cause I'm nice. You can call me {Name}.");
            int index = 1;
            foreach (var Item in Inventory)
            {
                WriteLine($"{index++}: {Item.Name} \n{Item.Description} - {Item.ItemWorth:c}");
            }
        }

    }
}
