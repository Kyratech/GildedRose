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

        //Function should return the difference in quality per day elapse before sell-by date
        public static readonly Dictionary<string, Func<Item, int>> ItemBaseQualityDegradeRates =
            new Dictionary<string, Func<Item, int>>
            {
                {"Aged Brie", (x) => 1},
                {"Backstage passes to a TAFKAL80ETC concert", (x) => PassBaseQualityDecay(x)}
            };

        //Function should return the ADDITIONAL difference in quality per day elapse after sell-by date
        public static readonly Dictionary<string, Func<Item, int>> ItemAdditionalQualityDegradeRates =
            new Dictionary<string, Func<Item, int>>
            {
                {"Aged Brie", (x) => 1},
                {"Backstage passes to a TAFKAL80ETC concert", (x) => -x.Quality}
            };

        //Function takes the standard decay rate and modies it somehow
        public static readonly Dictionary<string, Func<int, int>> ItemModifierDegradeEffects =
            new Dictionary<string, Func<int, int>>
            {
                {"Conjured", (x) => x * 2}
            };

        public static bool IsLegendary(string itemName)
        {
            return LegendaryItems.Contains(itemName);
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
