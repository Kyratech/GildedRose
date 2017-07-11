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

            if (ItemData.IsNotLegendary(nameParser.GetItemName()))
            {
                int baseDecay = BaseItemQualityDegrade(item, nameParser);
                StepQualityAndClamp(item, baseDecay);

                DegradeItemSellBy(item);

                if (HasGoneBad(item))
                {
                    int additionalDecay = AdditionalItemQualityDegrade(item, nameParser);
                    StepQualityAndClamp(item, additionalDecay);
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
                decay = ItemData.DefaultQualityDecay;
            }

            decay = ApplyModifierIfNecessary(decay, nameParser);

            return decay;
        }

        private void StepQualityAndClamp(Item item, int deltaQuality)
        {
            item.Quality = ClampQuality(item.Quality + deltaQuality);
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
                decay = ItemData.DefaultQualityDecay;
            }

            decay = ApplyModifierIfNecessary(decay, nameParser);

            return decay;
        }

        private int ApplyModifierIfNecessary(int decay, ItemNameParser nameParser)
        {
            if (nameParser.HasModifier())
            {
                return ItemData.ItemModifierDegradeEffects[nameParser.GetModifier()](decay);
            }
            else
            {
                return decay;
            }
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