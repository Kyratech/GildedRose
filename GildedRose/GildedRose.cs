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
            ItemNameParser nameParser = new ItemNameParser(item);

            if (!ItemData.IsLegendary(nameParser.GetItemName()))
            {
                StepQualityAndClamp(item, BaseItemQualityDegrade(item, nameParser));

                DegradeItemSellBy(item);

                if (HasGoneBad(item))
                {
                    StepQualityAndClamp(item, AdditionalItemQualityDegrade(item, nameParser));
                }
            }
        }

        private int BaseItemQualityDegrade(Item item, ItemNameParser nameParser)
        {
            int decay = 0;
 
            if (ItemData.ItemBaseQualityDegradeRates.ContainsKey(nameParser.GetItemName()))
            {
                decay = ItemData.ItemBaseQualityDegradeRates[nameParser.GetItemName()](item);
            }
            else
            {
                decay = -1;
            }

            if (nameParser.HasModifier())
            {
                decay = ItemData.ItemModifierDegradeEffects[nameParser.GetModifier()](decay);
            }

            return decay;
        }

        private void DegradeItemSellBy(Item item)
        {
            item.SellIn = item.SellIn - 1;
        }

        private bool HasGoneBad(Item item)
        {
            return (item.SellIn < 0);
        }

        private int AdditionalItemQualityDegrade(Item item, ItemNameParser nameParser)
        {
            int decay = 0;

            if (ItemData.ItemAdditionalQualityDegradeRates.ContainsKey(nameParser.GetItemName()))
            {
                decay = ItemData.ItemAdditionalQualityDegradeRates[nameParser.GetItemName()](item);
            }
            else
            {
                decay = -1;
            }

            if (nameParser.HasModifier())
            {
                decay = ItemData.ItemModifierDegradeEffects[nameParser.GetModifier()](decay);
            }

            return decay;
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