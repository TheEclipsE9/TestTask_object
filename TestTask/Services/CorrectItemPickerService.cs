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

        public List<Item> GetCorrectItemsWithoutSplitting(List<Item> itemList, double windowHeight)
        {
            var correctItems = new List<Item>();

            foreach (var item in itemList)
            {
                if (windowHeight >= item.H2-item.H1)
                {
                    correctItems.Add(item);
                }
            }

            return correctItems;
        }
        
        public List<Item> GetCorrectItemsWithSplitting()
        {
            var correctItems = new List<Item>();



            return correctItems;
        }
    }
}
