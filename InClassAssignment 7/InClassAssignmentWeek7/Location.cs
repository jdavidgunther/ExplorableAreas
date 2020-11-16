using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace InClassAssignmentWeek7
{
    //lots of properties that make our locations possible
    public class Location
    {
        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public List<Item> Items
        {
            get; set;
        }

        public ConsoleColor LocationColor
        {
            get; set;
        }

        public List<Item> RequiredItems = new List<Item>();

        public NPC Guardian { get; set; }

        //constructor :)
        public Location() { }
        public Location(string name, string description, List<Item> items, List<Item> requiredItems, ConsoleColor color, NPC guardian)
        {
            Name = name;
            Description = description;
            Items = items;
            RequiredItems = requiredItems;
            LocationColor = color;
            Guardian = guardian;
        }
        public Location(string name, string description, List<Item> items, ConsoleColor color)
        {
            Name = name;
            Description = description;
            Items = items;
            LocationColor = color;
        }

        //this prints the location and description to the console!
        public void PrintLocationInfo()
        {
            WriteLine($"You are at {Name}.  {Description}");
        }
    }
}