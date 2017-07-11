using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose
{
    class ItemNameParser
    {
        private readonly string modifier;
        private readonly string itemName;
        private readonly bool hasModifier;

        public ItemNameParser(Item item)
        {
            int firstSpaceIndex = item.Name.IndexOf(" ");
            string firstWord = firstSpaceIndex > -1 ? item.Name.Substring(0, firstSpaceIndex) : item.Name;

            if (ItemData.ItemModifierDegradeEffects.ContainsKey(firstWord))
            {
                hasModifier = true;
                modifier = firstWord;
                itemName = item.Name.Substring(firstSpaceIndex + 1);
            }
            else
            {
                hasModifier = false;
                modifier = "";
                itemName = item.Name;
            }
        }

        public string GetModifier()
        {
            return modifier;
        }

        public string GetItemName()
        {
            return itemName;
        }

        public bool HasModifier()
        {
            return hasModifier;
        }
    }
}
