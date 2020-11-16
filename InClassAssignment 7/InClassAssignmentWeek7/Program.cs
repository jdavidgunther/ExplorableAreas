/*
 * week 7 in class assignment
 * drivin by Ashlyn during class
 * 
 * Completed by Michael after class
 * 
 * Comments by Ashlyn Parsons :)
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using static System.Console;

namespace InClassAssignmentWeek7
{
    class Program
    {
        static void Main(string[] args)
        {
            //Motocylce count: 8
            Title = "Empty Jetpack Joyride";

            //This chunk of code instantiates all of our items in the game.  They have names and descriptions that will be passed in.
            Item CandyJetpack = new Item("Candy Jetpack", "It allows you to fly!  Unfortunately, fuel was not included.", 200);
            Item ClimbingGear = new Item("Climbing Gear", "No fuel required.", 40);
            Item CliffBar = new Item("Cliff Bar", "Candy made from cliffs", 2);
            Item MothersVase = new Item("Mother's Vase", "She loved this vase, could sell it for climbing gear though", 40);
            Item MothersOtherVase = new Item("Mother's Other Vase", "She loved this vase too, could sell it for a jetpack though", 200);
            Item Fuel = new Item("Hot Fudge", "Can be used as fuel.  Probably for a Jetpack but that's up to you.", 0);
            Item CandyCrown = new Item("Candy Crown", "The ultimate head gear.  Gives you all of the Candy that exists (And sovereignty over Candy World).", 10000000);
            Item AllTheCandy = new Item("All of the Candy", "Literally just all of it. Gives you access to the win command", 10000001);

            //Creating NPCs
            NPC ClimbingDan = new NPC()
            {
                Name = "Dan",
                Inventory = new List<Item> { ClimbingGear, CliffBar }

            };
            NPC MountainDaan = new NPC()
            {
                Name = "Daan",
                Inventory = new List<Item> { CandyJetpack }
            };
            NPC CottonCandyCloudDaaan = new NPC()
            {
                Name = "Daaan",
                Inventory = new List<Item> { CandyCrown }
            };
            NPC GhostDoon = new NPC()
            {
                Name = "Doon",
                Inventory = new List<Item> {}
            };

            //This is the same for the locations.  They have names, descriptions, items to pick up, required items for access, and colors.
            Location ClimbingStore = new Location("Climbing Store", "A store that serves all of your Candy Mountain climbing needs.", new List<Item> {}, new List<Item> { }, ConsoleColor.Cyan, ClimbingDan);
            Location CandyMountain = new Location("Candy Mountain", "It's a mountain.  Of candy. You might need something to help you get up there.", new List<Item> {}, new List<Item> {ClimbingGear}, ConsoleColor.Magenta, MountainDaan);
            Location House = new Location("Your House", "You live here!", new List<Item> {Fuel, MothersVase, MothersOtherVase}, ConsoleColor.Gray);

            //This is another way to create object without a method!
            Location CottonCandyCloud = new Location()
            {
                Name = "Cotton Candy Cloud",
                Description = "A piece of Cotton Candy so large and light that it just became a cloud. You would have to fly to get up to here.",
                Items = new List<Item> {},
                RequiredItems = new List<Item> { CandyJetpack, Fuel },
                LocationColor = ConsoleColor.Yellow,
                Guardian = CottonCandyCloudDaaan
            };
            Location CandyThrone = new Location()
            {
                Name = "The Candy Throne",
                Description = "A throne of candy. Only those who wear the candy crown can enter the candy throne.",
                Items = new List<Item> { AllTheCandy},
                RequiredItems = new List<Item> { CandyCrown},
                LocationColor = ConsoleColor.Cyan
            };



            //This is the list of locations in the game.
            List<Location> Locations = new List<Location>() {House, CandyMountain, ClimbingStore, CottonCandyCloud, CandyThrone};

            //The instantiation of an instance of the player class.  our current player.
            Player Player1 = new Player();

            //Our world has to be instantiated too, along with the important start up properties, such as the name of the world, the player, the location, etcetera.
            World world = new World("Candy World", Player1, House, Locations, AllTheCandy, CliffBar);
            //This takes us to the menu, and off we go!
            world.Menu();
        }
    }
}
