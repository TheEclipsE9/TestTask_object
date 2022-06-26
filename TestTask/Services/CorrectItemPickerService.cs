using TestTask.Entities;

namespace TestTask.Services
{
    public class CorrectItemPickerService
    {
        private readonly ItemSplitterService _itemSplitterService;

        public CorrectItemPickerService(ItemSplitterService itemSplitterService)
        {
            _itemSplitterService = itemSplitterService;
        }

        public List<Item> GetCorrectItemsWithoutSplitting(List<Item> itemList, double windowBottom, double windowTop)
        {
            var correctItems = new List<Item>();

            var windowHeight = windowTop - windowBottom;
            foreach (var item in itemList)
            {
                if (windowHeight >= item.H2-item.H1)
                {
                    correctItems.Add(item);
                }
            }

            return correctItems;
        }
        
        public List<Item> GetCorrectItemsWithSplitting(List<Item> itemList, double windowBottom, double windowTop)
        {
            var correctItems = new List<Item>();
            var itemsToSplit = new List<Item>();

            var itemsToCheck = new List<Item>(itemList);

            foreach (var item in itemList)
            {
                if (_itemSplitterService.ShouldBeSplitted(item, windowBottom, windowTop))
                {
                    itemsToSplit.Add(item);
                    itemsToCheck.Remove(item);
                }
            }

            foreach (var item in itemsToSplit)
            {
                var splitedItems = _itemSplitterService.SplitItem(item, windowBottom, windowTop);
                itemsToCheck.AddRange(splitedItems);
            }

            correctItems = GetCorrectItemsWithoutSplitting(itemsToCheck, windowBottom, windowTop);

            return correctItems;
        }
    }
}
