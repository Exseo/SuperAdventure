using System;
using System.Collections.Generic;
using System.Text;


namespace Engine
{
    class Weapon:Item //semicolon indicates the base class
    {
        //properties to generate for weapons
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public Weapon(int id, string name, string namePlural, int minimumDamage, int maximumDamge): base(id, name, namePlural)
        {
            MinimumDamage = minimumDamage;
            MaximumDamage = MaximumDamage;
        }
    }
}
