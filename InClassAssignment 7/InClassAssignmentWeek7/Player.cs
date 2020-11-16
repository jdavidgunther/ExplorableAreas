using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace InClassAssignmentWeek7
{
    public class Player
    {
        public string Name { get; set; }
        public Location CurrentLocation { get; set; }

        //creates player with default empty list
        public List<Item> Inventory = new List<Item>();

        //prints current player inventory to console
        public void PrintInventory()
        {
            //creates list of names of items
            List<string> names = new List<string>();

            //for each item in the player's inventory, this loop adds the name of the item to the inventory
            foreach (var item in Inventory)
            {
                names.Add(item.Name);
            }

            //this uses string.join to join the strings together and print them out with commas separating
            string ListOfNames = String.Join(", ", names);

            //inline if statement, "it looks a lot cooler, right?" (question mark operator)
            //uses question mark to check validity of the statement.  if it is true, it uses the first part.  if not, it uses the second.  Question mark acts like a boolean of sorts.  
            string ItemLabel = Inventory.Count == 1 ? "item" : "items";
            string Grammar = Inventory.Count == 1 ? "it is" : "they are";

            //this prints the inventory with the newly added information :)
            WriteLine($"You currently have {Inventory.Count} {ItemLabel} and {Grammar} {ListOfNames}.");



        }

        public void ShowInventory()
        {
            int index = 1;
            foreach (var Item in Inventory)
            {
                WriteLine($"{index++}: {Item.Name} \n{Item.Description} - {Item.ItemWorth:c}");
            }
        }

        //constructor :)
        public Player()
        {

        }

        //this method allows the player to collect an item at the current location
        public void CollectItem(Item item)
        {
            Inventory.Add(item);
            WriteLine($"You picked up {item.Name}.");
            PrintInventory();
        }

       
    }
}