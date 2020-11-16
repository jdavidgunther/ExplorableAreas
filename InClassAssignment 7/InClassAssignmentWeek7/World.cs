using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Console;

namespace InClassAssignmentWeek7
{
    public class World
    {
        public List<Location> Locations {get; set;}

        public string Name
        {
            get; set;
        }

        public Player CurrentPlayer
        {
            get; set;
        }
        public Item WinCondition { get; set; }
        public Item LoseCondition { get; set; }
        //this is where the world is defined.  there are lots of important properties that must be passed into the method, which happens in the program file.
        public World(string name, Player player, Location defaultLocation, List<Location> locations, Item winCondition, Item loseCondition)
        {
            Name = name;
            CurrentPlayer = player;
            CurrentPlayer.CurrentLocation = defaultLocation;
            Locations = locations;
            WinCondition = winCondition;
            LoseCondition = loseCondition;
            SetUpPlayer();
        }

        public void SetUpPlayer()
        {
            //where it all starts.  this is how the current player is set up!
            WriteLine($"Welcome to {Name}!");
            CurrentPlayer.Name = Utility.GetUserInput("What is your name?");
            WriteLine($"Your current location is {CurrentPlayer.CurrentLocation.Name}.");
        }

        public bool ChangeLocation()
        {
            int index = 1;
            foreach (var Location in Locations)
            {
                WriteLine($"{index++}: {Location.Name} \n{Location.Description}");
            }

            int userChoice = 0;
            string userInput = Utility.GetUserInput("Where would you like to go?");
            int.TryParse(userInput, out userChoice);
            if (userChoice > 0 && userChoice <= Locations.Count)
            {
                userChoice--;
                Location newLocation = Locations[userChoice];
                //this checks whether or not the player is in possesion of the required item, we start with true because there are some instances where a location has no required items.
                bool HasRequiredItems = true;

                //the loop checks to see if the items are NOT in the inventory.  the loop breaks when ONE item is not present.
                foreach (var Item in newLocation.RequiredItems)
                {
                    if (!CurrentPlayer.Inventory.Contains(Item))
                    {
                        HasRequiredItems = false;
                        break;
                    }
                }

                //if the player has all of the required items, they are allowed to go to the next area.  if not, the error message is displayed.
                if (HasRequiredItems)
                {
                    CurrentPlayer.CurrentLocation = newLocation;
                    return true;
                }
                else
                {
                    WriteLine("Uh oh.  You're missing something! You don't have what you need to go there.");
                    return false;
                }
            }
            WriteLine("That's not somewhere you can go");
            return false;

        }

        //this method shows the items available for collection at any location in the game.
        public void DisplayLocationItems()
        {
            //if the location still has the item that the player is trying to pick up, then they are displayed to the player with this method. 
            if (CurrentPlayer.CurrentLocation.Items.Count > 0)
            {
                int index = 1;
                WriteLine($"There are some items laying around.");
                //this tells the player which items are in their current location
                foreach (var Item in CurrentPlayer.CurrentLocation.Items)
                {
                    WriteLine($"{index++}: {Item.Name} \n{Item.Description}");
                }

                //the player then chooses an item to pick up.
                int userChoice = 0;
                string userInput = Utility.GetUserInput("Which item would you like to pick up?");
                int.TryParse(userInput, out userChoice);
                if (userChoice > 0 && userChoice <= CurrentPlayer.CurrentLocation.Items.Count)
                {
                    userChoice--;
                    Item PickedUpItem = CurrentPlayer.CurrentLocation.Items[userChoice];
                    CurrentPlayer.CollectItem(PickedUpItem);
                    CurrentPlayer.CurrentLocation.Items.Remove(PickedUpItem);
                }
                //this runs if the player inputs a number that is not applicable
                else
                {
                    WriteLine("That is not an available item");
                }

            }
            //this runs if the player has already picked up the item located here.
            else
            {
                WriteLine("There are no more items here.");
            }
        }
    
        public Boolean Trade(Item ItemToGet, Item ItemToSell)
        {
            //If the item that we want to trade has a great or equal value to the Item we want to get then the transaction will go through
            if (ItemToSell.ItemWorth >= ItemToGet.ItemWorth)
            {
                CurrentPlayer.CurrentLocation.Guardian.Inventory.Remove(ItemToGet);
                CurrentPlayer.Inventory.Remove(ItemToSell);
                CurrentPlayer.CurrentLocation.Guardian.Inventory.Add(ItemToSell);
                CurrentPlayer.Inventory.Add(ItemToGet);
                return true;
            }
            //
            else
            {
                WriteLine("You cannot afford this, what is this? It's not worth anything?");
                return false;
            }
        }

        //here we are!
        public void Menu()
        {
            Clear();
            Console.WriteLine($"Welcome {CurrentPlayer.Name} to the world of {Name}. " +
                $"\n\nThis is an text based game where you explore different areas using commands typed into a the console." +
                $"\nTo see the commands you can use type the word 'help' and hit enter. To enter other commands type in the command and hit enter." +
                $"\nIf you change the size of your window use the clear command to reset the top bar.\n\nHave fun");
            //the player starts with win the game being false.  it can only be changed to true when certain requirements have been met.
            bool WinTheGame = false;
            //they dont want to quit either!
            bool quit = false;
            //while win the game and quit are not true, these options will be presented to the player at the beginning of every turn.  
            while (!WinTheGame && !quit)
            {
                switch (ReadLine().ToLower().Trim())
                {
                    //this moves the player to their input
                    case "move":
                        if (ChangeLocation()) {
                        Console.ForegroundColor = CurrentPlayer.CurrentLocation.LocationColor;
                        Clear();
                        WriteLine($"You are now in {CurrentPlayer.CurrentLocation.Name}");
                        }
                        break;
                    //this allows the player to search a location
                    case "talk":
                        
                        if (CurrentPlayer.CurrentLocation.Guardian != null)
                        {
                            //Shows the inventory of the NPC
                            CurrentPlayer.CurrentLocation.Guardian.TalkAndShowInventory();
                            //Gets the player's choice for item to buy
                            int NPCInventoryChoice = Utility.GetUserInputInt("What item would like to get?");
                            //Reduces value by to match with index
                            NPCInventoryChoice--;
                            //Shows the inventory of the player
                            CurrentPlayer.ShowInventory();
                            //Gets the player's chocice for item to sell
                            int PlayerInventoryChoice = Utility.GetUserInputInt("What item would like to give?");
                            //Subtracts the choice by one to match the Index
                            PlayerInventoryChoice--;
                            //Checks to see if all chosen items are valid
                            if (NPCInventoryChoice >= 0 &&
                                NPCInventoryChoice < CurrentPlayer.CurrentLocation.Guardian.Inventory.Count &&
                                PlayerInventoryChoice >= 0 &&
                                PlayerInventoryChoice < CurrentPlayer.Inventory.Count)
                            {
                                //Sets the chosen items to 
                                Item ItemToGet = CurrentPlayer.CurrentLocation.Guardian.Inventory[NPCInventoryChoice];
                                Item ItemToSell = CurrentPlayer.Inventory[PlayerInventoryChoice];
                                if (Trade(ItemToGet, ItemToSell))
                                {
                                    CurrentPlayer.ShowInventory();
                                }
                            }
                            else
                            {
                                WriteLine($"Not a valid choice {CurrentPlayer.Name}");
                            }

                        }
                        else
                        {
                            WriteLine("Sorry stranger no Dans here");
                        }
                        break;
                    case "search":
                        DisplayLocationItems();
                        break;
                        //the player can clear the console if it gets too crowded
                    case "clear":
                        Clear();
                        break;
                        //this shows the commands available in case they get stuck
                    case "help":
                        if (CurrentPlayer.Inventory.Contains(LoseCondition))
                        {
                            WriteLine("Available Commands: Inventory, Talk, Search, Clear, Move, Lose");

                        }
                        else
                        {
                            WriteLine("Available Commands: Inventory, Talk, Search, Clear, Move, Win (you can only do this when you have all the candy), Quit (you don't want to do this)");
                        }
                        break;
                        //this displays their inventory
                    case "inventory":
                        CurrentPlayer.ShowInventory();
                        break;
                        //this command checks if the player has met all the requirements to win the game.
                    case "win":
                        //if the player has the correct items in their inventory, then they win the game!  if not, it will display an error message.
                        if (CurrentPlayer.Inventory.Contains(WinCondition))
                        {
                            WinTheGame = true;
                            WriteLine($"Hooray you collected all the candy and won the game. Press any key to close the console");
                            ReadKey();
                        }
                        else
                        {
                            WriteLine("Silly you cant do that yet. You haven't collected all the candy");
                        }
                        break;
                    //this is how the player can quit the game.
                    case "lose":
                        if (CurrentPlayer.Inventory.Contains(LoseCondition))
                        {
                            WinTheGame = false;
                            WriteLine($"You bought the cliff bar?!");
                            ReadKey();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            WriteLine("\n\n\n                                         You dummy...");
                            ReadKey();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            WriteLine($"\n\n\n\n\n          {CurrentPlayer.Name} perish");
                            ReadKey();
                            return;
                        }
                        else
                        {
                            WriteLine("You can still win, don't give up");
                        }
                        break;
                    case "quit":
                        if (Utility.Affirm("Are you sure you really want to quit?"))
                        {
                            quit = true;
                            WriteLine("Ok then. press any key to close the window");
                            ReadKey();
                        }
                        break;
                        //this catches if the player types an unknown command.
                    default:
                        Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
                        break;
                }
            }
        }
        //this clear method puts the bar of information at the top of the screen.
        public void Clear()
        {
            Console.Clear();
            Utility.Line();
            Utility.Center($"Player: {CurrentPlayer.Name}  Location: {CurrentPlayer.CurrentLocation.Name}  Inventory: {CurrentPlayer.Inventory.Count}");
            Utility.Line();
        }
        
        
    }
}