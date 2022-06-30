using NUnit.Framework;
using TestTask.Entities;
using TestTask.Services;

namespace TestTask.Tests.Integration.Services
{
    public class CorrectItemPickerServiceTest
    {
        [Test]
        [TestCaseSource(nameof(ItemCasesWithoutSplitting))]
        public void GetCorrectItemsWithoutSplitting(List<Item> itemList, double windowBottom, double windowTop, List<Item> expectedResult)
        {
            var splitterService = new ItemSplitterService();
            var pickerService = new CorrectItemPickerService(splitterService);


            var result = pickerService.GetCorrectItemsWithoutSplitting(itemList, windowBottom, windowTop);


            CollectionAssert.AreEquivalent(expectedResult, result, "Pick items that height is lower than window's height");
        }

        [Test]
        [TestCaseSource(nameof(ItemCasesWithSplitting))]
        public void GetCorrectItemsWithSplitting_SplitOne(List<Item> itemList, double windowBottom, double windowTop, List<Item> expectedList)
        {
            var splitterService = new ItemSplitterService();
            var pickerService = new CorrectItemPickerService(splitterService);


            var actionList = pickerService.GetCorrectItemsWithSplitting(itemList, windowBottom, windowTop);


            CollectionAssert.AreEquivalent(expectedList, actionList, "Pick items that height is lower than window's height or quantity is not correct!");
        }

        static object[] ItemCasesWithoutSplitting =
        {
            new object[] //TestCase 1 -- Only 2 items will be picked
            {
                new List<Item>
                {
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 2, 4),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 7),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 9, 15),
                },
                6,
                8,
                new List<Item>
                {
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 2, 4),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 7),
                },
            },
            new object[] //TestCase 3 -- Any will NOT be picked
            {
                new List<Item>
                {
                    new Item(11, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 2, 6),
                    new Item(14, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 8, 13),
                    new Item(8, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 0, 9),
                },
                10,
                13,
                new List<Item>
                {
                    //empty
                },
            },
            new object[] //TestCase 3 -- All will be picked
            {
                new List<Item>
                {
                    new Item(15, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 13, 15),
                    new Item(11, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 8, 12),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 20, 22),
                },
                12,
                16,
                new List<Item>
                {
                    new Item(15, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 13, 15),
                    new Item(11, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 8, 12),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 20, 22),
                },
            },
        };

        static object[] ItemCasesWithSplitting =
        {
            new object[] //TestCase 1 -- Split one
            {
                new List<Item>
                {
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 4), // picked without splitting
                    new Item(24, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 12), // split by bottom amd top
                    new Item(13, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 10, 15), // not picked without splitting
                },
                6,
                9,
                new List<Item>
                {
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 4),

                    new Item(8, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 6),
                    new Item(8, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 9),
                    new Item(8, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 9, 12),
                },
            },
            new object[] //TestCase 2 -- Split all
            {
                new List<Item>
                {
                    new Item(12, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 2, 14), // split by bottom and top
                    new Item(1, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 8, 12), // split by top
                    new Item(9, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 5, 8),// split by bottom
                },
                6,
                10,
                new List<Item>
                {
                    new Item(4, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 2, 6),
                    new Item(4, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 10),
                    new Item(4, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 10, 14),

                    new Item(0.5, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 8, 10),
                    new Item(0.5, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 10, 12),

                    new Item(3, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 5, 6),
                    new Item(6, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 8),
                },
            },
            new object[] //TestCase 3 -- do not split any | 3 picked
            {
                new List<Item>
                {
                    new Item(11, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 2, 3), // picked
                    new Item(12, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 11, 14), //picked
                    new Item(12, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 4, 10), // not picked
                    new Item(8, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 16, 18), // picked
                },
                10,
                15,
                new List<Item>
                {
                    new Item(11, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 2, 3),
                    new Item(12, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 11, 14),
                    new Item(8, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 16, 18),
                },
            },
        };
    }
}
