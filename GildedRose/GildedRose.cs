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
            if (ItemData.ItemBaseQualityDegrateRates.ContainsKey(item.Name))
            {
                StepQualityAndClamp(item, ItemData.ItemBaseQualityDegrateRates[item.Name](item));
            }
            else
            {
                StepQualityAndClamp(item, -1);
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
            if (ItemData.ItemAdditionalQualityDegrateRates.ContainsKey(item.Name))
            {
                StepQualityAndClamp(item, ItemData.ItemAdditionalQualityDegrateRates[item.Name](item));
            }
            else
            {
                StepQualityAndClamp(item, -1);
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