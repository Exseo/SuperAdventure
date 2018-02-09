using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace Engine
{
    public class HealingPotion : Item //semicolon indicates the base class
    {
        //properties to generate for Healing Potion
        public int AmountToHeal { get; set; }

        public HealingPotion (int id, string name, string namePlural, int amountToHeal) : base(id, name, namePlural)
        { AmountToHeal = amountToHeal; }

        
    }
}
