using NUnit.Framework;
using TestTask.Entities;
using TestTask.Services;

namespace TestTask.Tests.Services.Unit
{
    public class ItemSplitterServiceTest
    {
        [Test]
        [TestCaseSource(nameof(ShouldBeSplittedItemCases))]
        public void ShouldBeSplitted(Item item, double windowBottom, double windowTop, bool expectedResult)
        {
            var splitterService = new ItemSplitterService();

            var result = splitterService.ShouldBeSplitted(item, windowBottom, windowTop);

            Assert.AreEqual(expectedResult, result, "Object should be splitted if it can be splitted by window's borders");
        }

        [Test]
        [TestCaseSource(nameof(SplitItemCases_SplitItems))]
        public void SplitItem_SplitItems(Item item, double windowBottom, double windowTop, List<Item> expectedResult)
        {
            var splitterService = new ItemSplitterService();

            var result = splitterService.SplitItem(item, windowBottom, windowTop);
            
            CollectionAssert.AreEquivalent(expectedResult, result);
        }

        [Test]
        [TestCaseSource(nameof(SplitItemCases_DoNotSplitItems))]
        public void SplitItem_DoNotSplitItems(Item item, double windowBottom, double windowTop, List<Item> expectedResult)
        {
            var splitterService = new ItemSplitterService();

            var result = splitterService.SplitItem(item, windowBottom, windowTop);

            CollectionAssert.AreEquivalent(expectedResult, result);
        }



        static object[] ShouldBeSplittedItemCases =
        {
            new object[] //TestCase 1 -- should be splitted by top and bottom
            {
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 11),
                6,
                9,
                true
            },
            new object[] //TestCase 2 -- should be splitted by bottom 
            {
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 2, 5),
                4,
                6,
                true
            },
            new object[] //TestCase 3 -- should be splitted by top 
            {
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 8, 12),
                3,
                10,
                true
            },
            new object[] //TestCase 4 -- should be splitted by top and item.H1 == window.Bottom
            {
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 5, 11),
                5,
                9,
                true
            },
            new object[] //TestCase 5 -- should be splitted by bottom and item.H2 == window.Top
            {
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 5, 15),
                13,
                15,
                true
            },

            new object[] //TestCase 6 -- should Not be splitted: same size as window
            {
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 2, 4),
                2,
                4,
                false
            },
            new object[] //TestCase 7 -- should Not be splitted: item is below the window
            {
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 3),
                7,
                9,
                false
            },
            new object[] //TestCase 8 -- should Not be splitted: item is above the window 
            {
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 16, 17),
                10,
                15,
                false
            },
            new object[] //TestCase 9 -- should Not be splitted: item is below the window and item.H2 == window.Bottom
            {
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 2, 7),
                7,
                8,
                false
            },
            new object[] //TestCase 10 -- should Not be splitted: item is above the window and item.H1 == window.Top
            {
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 7, 10),
                5,
                7,
                false
            }
        };

        static object[] SplitItemCases_SplitItems =
        {
            new object[] //TestCase 1 -- split by by top and bottom => return 3 items
            {
                new Item(30, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 12),
                6,
                9,
                new List<Item>
                {
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 6),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 9),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 9, 12),
                },
            },
            new object[] //TestCase 2 -- split by by top => return 2 items
            {
                new Item(1, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 10),
                5,
                8,
                new List<Item>
                {
                    new Item(0.5, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 8),
                    new Item(0.5, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 8, 10),
                },
            },
            new object[] //TestCase 3 -- split by by bottom => return 2 items
            {
                new Item(1, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 9, 12),
                10,
                15,
                new List<Item>
                {
                    new Item(0.33, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 9, 10),
                    new Item(0.67, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 10, 12),
                },
            },
        };

        static object[] SplitItemCases_DoNotSplitItems =
        {
            new object[] //TestCase 1 -- do not split, item is below window.Bottom
            {
                new Item(24, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 5),
                6,
                9,
                new List<Item>
                {
                    new Item(24, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 5),
                },
            },
            new object[] //TestCase 2 -- do not split, item is above window.Top
            {
                new Item(34, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 11, 15),
                7,
                10,
                new List<Item>
                {
                    new Item(34, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 11, 15),
                },
            },
            new object[] //TestCase 3 -- do not split, item is in the window
            {
                new Item(12, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 4, 5),
                2,
                6,
                new List<Item>
                {
                    new Item(12, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 4, 5),
                },
            },
            new object[] //TestCase 4 -- do not split, item is the same as the window
            {
                new Item(20, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 11, 13),
                11,
                13,
                new List<Item>
                {
                    new Item(20, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 11, 13),
                },
            },
            new object[] //TestCase 5 -- do not split, item is below the window and item.H2 == window.Bottom
            {
                new Item(20, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 7),
                7,
                13,
                new List<Item>
                {
                    new Item(20, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 7),
                },
            },
            new object[] //TestCase 5 -- do not split, item is above the window and item.H1 == window.Top
            {
                new Item(7, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 18, 25),
                14,
                18,
                new List<Item>
                {
                    new Item(7, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 18, 25),
                },
            },
        };
    }
}
