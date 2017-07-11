using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose
{
    class ItemData
    {
        public static readonly IList<string> LegendaryItems = new ReadOnlyCollection<string>(new List<string>
        {
            "Sulfuras, Hand of Ragnaros"
        });

        public const int MaxQuality = 50;
        public const int MinQuality = 0;

        public static bool IsLegendary(Item item)
        {
            return LegendaryItems.Contains(item.Name);
        }
    }
}
