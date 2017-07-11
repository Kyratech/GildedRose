using System.Collections.Generic;
using NUnit.Framework;

namespace GildedRose
{
    [TestFixture()]
    public class GildedRoseTest
    {
        [Test()]
        public void foo()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            global::GildedRose.GildedRose app = new global::GildedRose.GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual("foo", Items[0].Name);
        }
    }
}