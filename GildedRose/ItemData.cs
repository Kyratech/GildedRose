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
        public static readonly IList<string> ItemsThatIncreaseInQuality = new ReadOnlyCollection<string>(new List<string>
        {
            "Aged Brie",
            "Backstage passes to a TAFKAL80ETC concert"
        });

        public static readonly IList<string> ItemsWithUnchangingQuality = new ReadOnlyCollection<string>(new List<string>
        {
            "Sulfuras, Hand of Ragnaros"
        });

        public const int MaxQuality = 50;
        public const int MinQuality = 0;

        public static bool IsAnItemThatIncreasesInQuality(Item item)
        {
            return ItemsThatIncreaseInQuality.Contains(item.Name);
        }

        public static bool IsAnItemWithUnchangingQuality(Item item)
        {
            return ItemsWithUnchangingQuality.Contains(item.Name);
        }
    }
}
