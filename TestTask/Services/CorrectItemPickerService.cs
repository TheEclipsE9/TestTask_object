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

        public List<Item> GetCorrectItemsWithoutSplitting()
        {
            var correctItems = new List<Item>();



            return correctItems;
        }
        
        public List<Item> GetCorrectItemsWithSplitting()
        {
            var correctItems = new List<Item>();



            return correctItems;
        }
    }
}
