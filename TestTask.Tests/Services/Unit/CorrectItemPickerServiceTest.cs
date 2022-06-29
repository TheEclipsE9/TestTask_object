using Moq;
using NUnit.Framework;
using TestTask.Entities;
using TestTask.Services;

namespace TestTask.Tests.Services.Unit
{
    public class CorrectItemPickerServiceTest
    {
        [Test]
        [TestCaseSource(nameof(ItemCasesWithoutSplitting))]
        public void GetCorrectItemsWithoutSplitting(List<Item> itemList, double windowBottom, double windowTop, List<Item> expectedResult)
        {
            var splitterServiceMock = new Mock<IItemSplitterService>();
            var pickerService = new CorrectItemPickerService(splitterServiceMock.Object);


            var result = pickerService.GetCorrectItemsWithoutSplitting(itemList, windowBottom, windowTop);


            CollectionAssert.AreEquivalent(expectedResult, result, "Pick items that height is lower than window's height");
        }

        [Test]
        [TestCaseSource(nameof(ItemCasesWithSplitting))]
        public void GetCorrectItemsWithSplitting_SplitOne(List<Item> itemList, double windowBottom, double windowTop, List<Item> expectedResult)
        {
            var splitterServiceMock = new Mock<IItemSplitterService>();
            splitterServiceMock.SetupSequence(z => z.ShouldBeSplitted(It.IsAny<Item>(), It.IsAny<double>(), It.IsAny<double>())).Returns(false).Returns(true).Returns(false);
            splitterServiceMock.Setup(z => z.SplitItem(It.IsAny<Item>(), It.IsAny<double>(), It.IsAny<double>())).Returns(expectedResult);

            var pickerService = new CorrectItemPickerService(splitterServiceMock.Object);


            var result = pickerService.GetCorrectItemsWithSplitting(itemList, windowBottom, windowTop);


            splitterServiceMock.Verify(z => z.ShouldBeSplitted(It.IsAny<Item>(), It.IsAny<double>(), It.IsAny<double>()), Times.Exactly(3));
            splitterServiceMock.Verify(z => z.SplitItem(itemList[1], It.IsAny<double>(), It.IsAny<double>()), Times.Exactly(1));
        }

        [Test]
        [TestCaseSource(nameof(ItemCasesWithSplitting))]
        public void GetCorrectItemsWithSplitting_SplitAll(List<Item> itemList, double windowBottom, double windowTop, List<Item> expectedResult)
        {
            var splitterServiceMock = new Mock<IItemSplitterService>();
            splitterServiceMock.SetupSequence(z => z.ShouldBeSplitted(It.IsAny<Item>(), It.IsAny<double>(), It.IsAny<double>())).Returns(true).Returns(true).Returns(true);
            splitterServiceMock.Setup(z => z.SplitItem(It.IsAny<Item>(), It.IsAny<double>(), It.IsAny<double>())).Returns(expectedResult);

            var pickerService = new CorrectItemPickerService(splitterServiceMock.Object);


            var result = pickerService.GetCorrectItemsWithSplitting(itemList, windowBottom, windowTop);


            splitterServiceMock.Verify(z => z.ShouldBeSplitted(It.IsAny<Item>(), It.IsAny<double>(), It.IsAny<double>()), Times.Exactly(3));
            splitterServiceMock.Verify(z => z.SplitItem(It.IsAny<Item>(), It.IsAny<double>(), It.IsAny<double>()), Times.Exactly(3));
        }

        [Test]
        [TestCaseSource(nameof(ItemCasesWithSplitting))]
        public void GetCorrectItemsWithSplitting_DoNotSplitAny(List<Item> itemList, double windowBottom, double windowTop, List<Item> expectedResult)
        {
            var splitterServiceMock = new Mock<IItemSplitterService>();
            splitterServiceMock.SetupSequence(z => z.ShouldBeSplitted(It.IsAny<Item>(), It.IsAny<double>(), It.IsAny<double>())).Returns(false).Returns(false).Returns(false);
            splitterServiceMock.Setup(z => z.SplitItem(It.IsAny<Item>(), It.IsAny<double>(), It.IsAny<double>())).Returns(expectedResult);

            var pickerService = new CorrectItemPickerService(splitterServiceMock.Object);


            var result = pickerService.GetCorrectItemsWithSplitting(itemList, windowBottom, windowTop);


            splitterServiceMock.Verify(z => z.ShouldBeSplitted(It.IsAny<Item>(), It.IsAny<double>(), It.IsAny<double>()), Times.Exactly(3));
            splitterServiceMock.Verify(z => z.SplitItem(It.IsAny<Item>(), It.IsAny<double>(), It.IsAny<double>()), Times.Never());
        }

        static object[] ItemCasesWithoutSplitting =
        {
            new object[] //TestCase 1 -- Only 2 items will be picked
            {
                new List<Item>
                {
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 3),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 9, 13),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 9),
                },
                6,
                9,
                new List<Item>
                {
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 3),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 9),
                },
            },
            new object[] //TestCase 2 -- Any item will not be picked
            {
                new List<Item>
                {
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 5),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 11),
                    new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 0, 9),
                },
                15,
                18,
                new List<Item>
                {
                    //empty
                },
            },
            new object[] //TestCase 3 -- All items will be picked
            {
                new List<Item>
                {
                    new Item(15, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 2, 4),
                    new Item(17, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 11, 13),
                    new Item(11, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 10, 13),
                    new Item(23, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 15, 16),
                },
                10,
                13,
                new List<Item>
                {
                    new Item(15, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 2, 4),
                    new Item(17, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 11, 13),
                    new Item(11, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 10, 13),
                    new Item(23, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 15, 16),
                },
            },
        };

        static object[] ItemCasesWithSplitting =
        {
            new object[]
            {
                new List<Item>
                {
                    new Item(24, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 5),
                    new Item(24, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 12),
                    new Item(24, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 10, 14),
                },
                6,
                9,
                new List<Item>
                {
                    new Item(8, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 6),
                    new Item(8, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 6, 9),
                    new Item(8, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 9, 12),
                }
            },
        };
    }
}
