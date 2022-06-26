using TestTask.Entities;
using TestTask.Services;

namespace TestTask
{
    class Program
    {
        static void Main()
        {
            var itemList = new List<Item>
            {
                new Item(10, Types.ItemType.TypeA, DateTime.Now, DateTime.Now.AddMinutes(10), 3, 6),
                new Item(15, Types.ItemType.TypeB, DateTime.Now, DateTime.Now.AddMinutes(10), 1, 4),
                new Item(20, Types.ItemType.TypeC, DateTime.Now, DateTime.Now.AddMinutes(10), 6, 8),
            };
            var window = new Window(4, 7);
            var result = new List<Item>();

            var _itemSplitterService = new ItemSplitterService();
            var _correctItemPickerService = new CorrectItemPickerService(_itemSplitterService);

            result = _correctItemPickerService.GetCorrectItemsWithoutSplitting(itemList, window.GetWindowHeight());
            foreach (var item in result)
            {
                Console.WriteLine($"Type: { item.Type}| H1: {item.H1} | H2: {item.H2} | Weight: {item.Weight}");
            }
            _correctItemPickerService.GetCorrectItemsWithSplitting();
        }
    }
}