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
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 3), // picked without splitting
                new Item(15, Types.ItemType.TypeB, DateTime.Today, DateTime.Today.AddDays(1), 1, 5), // not picked
                new Item(20, Types.ItemType.TypeC, DateTime.Today, DateTime.Today.AddDays(1), 4, 8), // splitted for 2 -- all 2 picked
                new Item(24, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 12), // splitted for 3 -- all 3 picked
                new Item(23, Types.ItemType.TypeC, DateTime.Today, DateTime.Today.AddDays(1), 1, 18), // splitted for 3 -- only 1 picked
            };
            var window = new Window(6, 9);

            
            var _itemSplitterService = new ItemSplitterService();
            var _correctItemPickerService = new CorrectItemPickerService(_itemSplitterService);

            var resultWithoutSpliting = _correctItemPickerService.GetCorrectItemsWithoutSplitting(itemList, window.Bottom, window.Top);
            var resultWithSplitting = _correctItemPickerService.GetCorrectItemsWithSplitting(itemList, window.Bottom, window.Top);

            ResultPrinter(resultWithoutSpliting, "Without spliting");
            ResultPrinter(resultWithSplitting, "With spliting");
        }

        private static void ResultPrinter(List<Item> result, string message)
        {
            Console.WriteLine($"Result {message}:");
            foreach (var item in result)
            {
                Console.WriteLine($"Type: {item.Type} | H1: {item.H1} | H2: {item.H2} | Weight: {item.Weight}");
            }
            Console.WriteLine("");
        }
    }
}