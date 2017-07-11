using System.Collections.Generic;
using NUnit.Framework;

namespace GildedRose
{
    [TestFixture()]
    public class GildedRoseTest
    {
        [Test()]
        public void ItemsAddedToInventory()
        {
            IList<Item> items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            global::GildedRose.GildedRose app = new global::GildedRose.GildedRose(items);
            app.UpdateQuality();
            Assert.AreEqual("foo", items[0].Name);
        }

        [Test()]
        public void SellByDateDegrades()
        {
            Item testItem = new Item {Name = "test", SellIn = 10, Quality = 10};
            IList<Item> items = new List<Item> { testItem };

            GildedRose app = new GildedRose(items);
            
            Assert.AreEqual(10, items[0].SellIn);
            app.UpdateQuality();
            Assert.AreEqual(9, items[0].SellIn);
            app.UpdateQuality();
            Assert.AreEqual(8, items[0].SellIn);
        }

        [Test()]
        public void QualityDegrades()
        {
            Item testItem = new Item { Name = "test", SellIn = 10, Quality = 10 };
            IList<Item> items = new List<Item> { testItem };

            GildedRose app = new GildedRose(items);

            Assert.AreEqual(10, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(9, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(8, items[0].Quality);
        }

        [Test()]
        public void QualityDegradesFasterAfterSellByDate()
        {
            Item testItem = new Item { Name = "test", SellIn = 1, Quality = 10 };
            IList<Item> items = new List<Item> { testItem };

            GildedRose app = new GildedRose(items);

            Assert.AreEqual(10, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(9, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(7, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(5, items[0].Quality);
        }

        [Test()]
        public void QualityIsNeverNegative()
        {
            Item testItem = new Item { Name = "test", SellIn = 10, Quality = 1 };
            IList<Item> items = new List<Item> { testItem };

            GildedRose app = new GildedRose(items);

            Assert.AreEqual(1, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(0, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(0, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(0, items[0].Quality);
        }

        [Test()]
        public void AgedBrieIncreasesQuality()
        {
            Item brieItem = new Item { Name = "Aged Brie", SellIn = 10, Quality = 10 };
            IList<Item> items = new List<Item> { brieItem };

            GildedRose app = new GildedRose(items);

            Assert.AreEqual(10, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(11, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(12, items[0].Quality);
        }

        [Test()]
        public void AgedBrieIncreasesFasterAffterSellByDate()
        {
            Item brieItem = new Item { Name = "Aged Brie", SellIn = 1, Quality = 10 };
            IList<Item> items = new List<Item> { brieItem };

            GildedRose app = new GildedRose(items);

            Assert.AreEqual(10, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(11, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(13, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(15, items[0].Quality);
        }

        [Test()]
        public void QualityIsNeverMoreThan50()
        {
            Item brieItem = new Item { Name = "Aged Brie", SellIn = 10, Quality = 49 };
            Item passItem = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 49 };
            IList<Item> items = new List<Item> { brieItem, passItem };

            GildedRose app = new GildedRose(items);

            Assert.AreEqual(49, items[0].Quality);
            Assert.AreEqual(49, items[1].Quality);
            app.UpdateQuality();
            Assert.AreEqual(50, items[0].Quality);
            Assert.AreEqual(50, items[1].Quality);
            app.UpdateQuality();
            Assert.AreEqual(50, items[0].Quality);
            Assert.AreEqual(50, items[1].Quality);
            app.UpdateQuality();
            Assert.AreEqual(50, items[0].Quality);
            Assert.AreEqual(50, items[1].Quality);
        }

        [Test()]
        public void SulfurasSellByNeverDegrades()
        {
            Item legendItem = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 10 };
            IList<Item> items = new List<Item> { legendItem };

            GildedRose app = new GildedRose(items);

            Assert.AreEqual(10, items[0].SellIn);
            app.UpdateQuality();
            Assert.AreEqual(10, items[0].SellIn);
            app.UpdateQuality();
            Assert.AreEqual(10, items[0].SellIn);
            app.UpdateQuality();
            Assert.AreEqual(10, items[0].SellIn);
        }

        [Test()]
        public void SulfurasQualityNeverDegrades()
        {
            Item legendItem = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 };
            IList<Item> items = new List<Item> { legendItem };

            GildedRose app = new GildedRose(items);

            Assert.AreEqual(80, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(80, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(80, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(80, items[0].Quality);
        }

        [Test()]
        public void BackstagePassesQualityRamp()
        {
            Item passItem = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 10 };
            IList<Item> items = new List<Item> { passItem };

            GildedRose app = new GildedRose(items);

            //Increases by 1 until 10 days before
            Assert.AreEqual(10, items[0].Quality);
            app.UpdateQuality();//14 Days left
            Assert.AreEqual(11, items[0].Quality);
            app.UpdateQuality();//13
            Assert.AreEqual(12, items[0].Quality);

            app.UpdateQuality();//12
            app.UpdateQuality();//11
            app.UpdateQuality();//10

            //Increases by 2 when less than 10 days left
            Assert.AreEqual(15, items[0].Quality);
            app.UpdateQuality();//9
            Assert.AreEqual(17, items[0].Quality);
            app.UpdateQuality();//8
            Assert.AreEqual(19, items[0].Quality);

            app.UpdateQuality();//7
            app.UpdateQuality();//6
            app.UpdateQuality();//5

            //Increases by 3 when less than 5 days left
            Assert.AreEqual(25, items[0].Quality);
            app.UpdateQuality();//4
            Assert.AreEqual(28, items[0].Quality);
            app.UpdateQuality();//3
            Assert.AreEqual(31, items[0].Quality);

            app.UpdateQuality();//2
            app.UpdateQuality();//1
            app.UpdateQuality();//0

            //Drop to zero after concert
            Assert.AreEqual(40, items[0].Quality);
            app.UpdateQuality();//-1 Days left (concert yesterday)
            Assert.AreEqual(0, items[0].Quality);
        }

        [Test()]
        public void CanIdentifyConjuredItems()
        {
            Item normalItem = new Item { Name = "Test", SellIn = 10, Quality = 10 };
            Item conjuredItem = new Item { Name = "Conjured Test", SellIn = 10, Quality = 10 };
            Item normalItemMultipleWords = new Item { Name = "Test Item", SellIn = 10, Quality = 10 };
            Item conjuredItemMultipleWords = new Item { Name = "Conjured Test Item", SellIn = 10, Quality = 10 };

            ItemNameParser normalNameParser = new ItemNameParser(normalItem);
            ItemNameParser conjuredNameParser = new ItemNameParser(conjuredItem);
            ItemNameParser normalMultipleWordsNameParser = new ItemNameParser(normalItemMultipleWords);
            ItemNameParser conjuredMultipleWordsNameParser = new ItemNameParser(conjuredItemMultipleWords);

            Assert.AreEqual(false, normalNameParser.HasModifier());
            StringAssert.AreEqualIgnoringCase("Test", normalNameParser.GetItemName());

            Assert.AreEqual(true, conjuredNameParser.HasModifier());
            StringAssert.AreEqualIgnoringCase("Conjured", conjuredNameParser.GetModifier());
            StringAssert.AreEqualIgnoringCase("Test", conjuredNameParser.GetItemName());

            Assert.AreEqual(false, normalMultipleWordsNameParser.HasModifier());
            StringAssert.AreEqualIgnoringCase("Test Item", normalMultipleWordsNameParser.GetItemName());

            Assert.AreEqual(true, conjuredMultipleWordsNameParser.HasModifier());
            StringAssert.AreEqualIgnoringCase("Conjured", conjuredMultipleWordsNameParser.GetModifier());
            StringAssert.AreEqualIgnoringCase("Test Item", conjuredMultipleWordsNameParser.GetItemName());
        }

        [Test()]
        public void ConjuredItemsDegradeTwiceAsFast()
        {
            TestConjuredItemQualityDegrade();
            TestConjuredBrieQualityDegrade();
            TestConjuredPassQualityDegrade();
            TestConjuredLegendQualityDegrade();
        }

        private void TestConjuredItemQualityDegrade()
        {
            Item testItem = new Item { Name = "Conjured Test", SellIn = 10, Quality = 10 };
            IList<Item> items = new List<Item> { testItem };

            GildedRose app = new GildedRose(items);

            Assert.AreEqual(10, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(8, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(6, items[0].Quality);

            testItem = new Item { Name = "Conjured Test", SellIn = 0, Quality = 10 };
            items = new List<Item> { testItem };
            app = new GildedRose(items);

            Assert.AreEqual(10, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(6, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(2, items[0].Quality);
        }

        private void TestConjuredBrieQualityDegrade()
        {
            Item brieItem = new Item { Name = "Conjured Aged Brie", SellIn = 10, Quality = 10 };
            IList<Item> items = new List<Item> { brieItem };

            GildedRose app = new GildedRose(items);

            Assert.AreEqual(10, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(12, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(14, items[0].Quality);

            brieItem = new Item { Name = "Conjured Aged Brie", SellIn = 0, Quality = 10 };
            items = new List<Item> { brieItem };
            app = new GildedRose(items);

            Assert.AreEqual(10, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(14, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(18, items[0].Quality);
        }

        private void TestConjuredPassQualityDegrade()
        {
            Item passItem = new Item { Name = "Conjured Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 10 };
            IList<Item> items = new List<Item> { passItem };

            GildedRose app = new GildedRose(items);

            //Increases by 1 until 10 days before
            Assert.AreEqual(10, items[0].Quality);
            app.UpdateQuality();//14 Days left
            Assert.AreEqual(12, items[0].Quality);
            app.UpdateQuality();//13
            Assert.AreEqual(14, items[0].Quality);

            app.UpdateQuality();//12
            app.UpdateQuality();//11
            app.UpdateQuality();//10

            //Increases by 2 when less than 10 days left
            Assert.AreEqual(20, items[0].Quality);
            app.UpdateQuality();//9
            Assert.AreEqual(24, items[0].Quality);
            app.UpdateQuality();//8
            Assert.AreEqual(28, items[0].Quality);

            app.UpdateQuality();//7
            app.UpdateQuality();//6
            app.UpdateQuality();//5

            //Increases by 3 when less than 5 days left
            Assert.AreEqual(40, items[0].Quality);
            app.UpdateQuality();//4
            Assert.AreEqual(46, items[0].Quality);
            app.UpdateQuality();//3
            Assert.AreEqual(50, items[0].Quality);

            app.UpdateQuality();//2
            app.UpdateQuality();//1
            app.UpdateQuality();//0

            //Drop to zero after concert
            Assert.AreEqual(50, items[0].Quality);
            app.UpdateQuality();//-1 Days left (concert yesterday)
            Assert.AreEqual(0, items[0].Quality);
        }

        private void TestConjuredLegendQualityDegrade()
        {
            Item legendItem = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 };
            IList<Item> items = new List<Item> { legendItem };

            GildedRose app = new GildedRose(items);

            Assert.AreEqual(80, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(80, items[0].Quality);
            app.UpdateQuality();
            Assert.AreEqual(80, items[0].Quality);
        }
    }
}