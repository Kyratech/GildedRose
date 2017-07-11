using System;
using System.Collections.Generic;

namespace GildedRose
{
    class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                UpdateItemQuality(Items[i]);
            }
        }

        private void UpdateItemQuality(Item item)
        {
            if (!ItemData.IsLegendary(item))
            {
                BaseItemQualityDegrade(item);

                DegradeItemSellBy(item);

                if (HasGoneBad(item))
                {
                    AdditionalItemQualityDegrade(item);
                }
            }
        }

        private void BaseItemQualityDegrade(Item item)
        {
            switch (item.Name)
            {
                case "Aged Brie":
                    StepQualityAndClamp(item, 1);
                    break;
                case "Backstage passes to a TAFKAL80ETC concert":
                    StepQualityAndClamp(item, 1);
                    if (item.SellIn < 11)
                    {
                        StepQualityAndClamp(item, 1);
                    }

                    if (item.SellIn < 6)
                    {
                        StepQualityAndClamp(item, 1);
                    }
                    break;
                default:
                    StepQualityAndClamp(item, -1);
                    break;
            }
        }

        private void DegradeItemSellBy(Item item)
        {
            item.SellIn = item.SellIn - 1;
        }

        private bool HasGoneBad(Item item)
        {
            return (item.SellIn < 0);
        }

        private void AdditionalItemQualityDegrade(Item item)
        {
            switch (item.Name)
            {
                case "Aged Brie":
                    StepQualityAndClamp(item, 1);
                    break;
                case "Backstage passes to a TAFKAL80ETC concert":
                    item.Quality = 0;
                    break;
                default:
                    StepQualityAndClamp(item, -1);
                    break;
            }
        }

        private void StepQualityAndClamp(Item item, int deltaQuality)
        {
            item.Quality = ClampQuality(item.Quality + deltaQuality);
        }

        private int ClampQuality(int quality)
        {
            if (quality < ItemData.MinQuality)
            {
                return ItemData.MinQuality;
            }
            else if (quality > ItemData.MaxQuality)
            {
                return ItemData.MaxQuality;
            }
            else
            {
                return quality;
            }
        }
    }
}