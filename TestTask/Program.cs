using TestTask.Entities;
using TestTask.Services;

namespace TestTask
{
    class Program
    {
        static void Main()
        {
            var itemList = new List<Item>();
            var window = new Window(0, 0);

            var _itemSplitterService = new ItemSplitterService();
            var _correctItemPickerService = new CorrectItemPickerService(_itemSplitterService);

            _correctItemPickerService.GetCorrectItemsWithoutSplitting();
            _correctItemPickerService.GetCorrectItemsWithSplitting();
        }
    }
}