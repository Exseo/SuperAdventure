using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace Engine
{
    public class Location
    {
        //properties to generate for location
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //using classes at datatypes to assign properties to location
        public Item ItemRequiredToEnter { get; set; }
        public Quest QuestAvailableHere { get; set; }
        public Monster MonsterLivingHere { get; set; }
        public Location LocationToTheNorth { get; set; }
        public Location LocationToTheEast { get; set; }
        public Location LocationToTheSouth { get; set; }
        public Location LocationToTheWest { get; set; }

        //constructor code for the Location class
        public Location(int id, string name, string description,
            Item itemRequiredToEnter = null, Quest questAvailableHere = null, Monster monsterLivingHere = null)
        {
            ID = id;
            Name = name;
            Description = description;
            ItemRequiredToEnter = itemRequiredToEnter;
            QuestAvailableHere = questAvailableHere;
            MonsterLivingHere = monsterLivingHere;

        }

    }
}
