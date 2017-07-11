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

        public static readonly Dictionary<string, Func<Item, int>> ItemBaseQualityDegrateRates =
            new Dictionary<string, Func<Item, int>>
            {
                {"Aged Brie", (x) => 1},
                {"Backstage passes to a TAFKAL80ETC concert", (x) => PassBaseQualityDecay(x)}
            };

        public static readonly Dictionary<string, Func<Item, int>> ItemAdditionalQualityDegrateRates =
            new Dictionary<string, Func<Item, int>>
            {
                {"Aged Brie", (x) => 1},
                {"Backstage passes to a TAFKAL80ETC concert", (x) => -x.Quality}
            };

        public static bool IsLegendary(Item item)
        {
            return LegendaryItems.Contains(item.Name);
        }

        private static int PassBaseQualityDecay(Item item)
        {
            if (item.SellIn > 10)
            {
                return 1;
            }
            else if (item.SellIn > 5)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}
