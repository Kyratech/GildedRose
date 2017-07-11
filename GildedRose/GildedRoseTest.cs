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
    }
}