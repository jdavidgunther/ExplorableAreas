using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace InClassAssignmentWeek7
{
    public class Item
    {
        //nothing much to see here.  this class is full of properties that make our items run nicely.
        public Item(string name, string description, int itemWorth)
        {
            Name = name;
            Description = description;
            ItemWorth = itemWorth;
        }

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public int ItemWorth { get; set; }
    }
}