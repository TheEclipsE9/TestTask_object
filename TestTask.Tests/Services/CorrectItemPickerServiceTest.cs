using Moq;
using NUnit.Framework;
using TestTask.Entities;
using TestTask.Services;

namespace TestTask.Tests.Services
{
    public class CorrectItemPickerServiceTest
    {
        [Test]
        [TestCaseSource(nameof(ItemCasesWithoutSplitting))]
        public void GetCorrectItemsWithoutSplitting(List<Item> itemList, double windowBottom, double windowTop, List<Item> expectedList)
        {
            //prepare
            
            //act

            //assert
        }

        [Test]
        [TestCaseSource(nameof(ItemCasesWithSplitting))]
        public void GetCorrectItemsWithSplitting(List<Item> itemList, double windowBottom, double windowTop, List<Item> expectedList)
        {
            //prepare

            //act

            //assert
        }

        static object[] ItemCasesWithoutSplitting =
        {
            new object[] //TestCase 1 -- Only 2 items will be picked
            {
                new List<Item> // input list
                {
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 3),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 9, 13),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 9),
                },
                6,             // windowBottom
                9,             // windowTop
                new List<Item> // output list
                {
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 3),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 9),
                },
            },
            new object[] //TestCase 2 -- Any item will not be picked
            {
                new List<Item> // input list
                {
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 5),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 11),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 0, 9),
                },
                6,             // windowBottom
                9,             // windowTop
                new List<Item> // output list
                {
                    //empty
                },
            },
        };

        static object[] ItemCasesWithSplitting =
        {
            new object[] //TestCase 1 -- Only 2 items will be picked
            {
                new List<Item> // input list
                {
                    new Item(24, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 12),
                },
                6,             // windowBottom
                9,             // windowTop
                new List<Item> // output list
                {
                    new Item(8, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 6),
                    new Item(8, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 9),
                    new Item(8, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 9, 12),
                },
            },
        };
    }
}
