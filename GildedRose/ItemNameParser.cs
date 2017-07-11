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
            string firstWord = GetFirstWord(item.Name);

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

        private string GetFirstWord(string itemName)
        {
            int firstSpaceIndex = itemName.IndexOf(" ");

            if (firstSpaceIndex > -1)
            {
                return itemName.Substring(0, firstSpaceIndex);
            }
            else
            {
                return itemName;
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
