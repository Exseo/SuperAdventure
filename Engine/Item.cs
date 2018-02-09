using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace Engine
{
    public class Item
    {
        //properties to generate for Items
        public int ID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }

        public Item(int id, string name, string namePlural)
        {
            ID = id;
            Name = name;
            NamePlural = namePlural;
        }
    }
}
