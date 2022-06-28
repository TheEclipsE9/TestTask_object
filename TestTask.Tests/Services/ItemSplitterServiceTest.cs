using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Entities;
using TestTask.Services;

namespace TestTask.Tests.Services
{
    public class ItemSplitterServiceTest
    {
        [Test]
        [TestCaseSource(nameof(ShuldBeSplittedItemCases))]
        public void ShouldBeSplitted(Item item, double windowBottom, double windowTop)
        {
            //prepare
            var splitterService = new ItemSplitterService();


            //act
            var result = splitterService.ShouldBeSplitted(item, windowBottom, windowTop);

            //assert
            Assert.AreEqual(true, result, "Object should be splitted if it can be splitted by window");
        }

        [Test]
        [TestCaseSource(nameof(SplitItemCases))]
        public void SplitItem(Item item, double windowBottom, double windowTop, List<Item> expectedList)
        {
            //prepare
            var splitterService = new ItemSplitterService();


            //act
            var result = splitterService.SplitItem(item, windowBottom, windowTop);

            //assert
            CollectionAssert.AreEquivalent(expectedList, result);
        }

        static object[] ShuldBeSplittedItemCases =
        {
            new object[] //TestCase 1
            {
                // input item
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 11),                
                6,             // windowBottom
                9,             // windowTop
            },
            new object[] //TestCase 2
            {
                //input item
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 5, 8),
                6,             // windowBottom
                9,             // windowTop
            }
        };

        static object[] SplitItemCases =
        {
            new object[] //TestCase 1
            {
                // input item
                new Item(30, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 12),
                6,             // windowBottom
                9,             // windowTop
                new List<Item> // output list
                {
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 6),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 9),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 9, 12),
                },
            },
        };
    }
}
