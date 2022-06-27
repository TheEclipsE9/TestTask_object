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
                new Item(10, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 1, 3), //проходит без разделения
                new Item(15, Types.ItemType.TypeB, DateTime.Today, DateTime.Today.AddDays(1), 1, 5), //не проходит
                new Item(20, Types.ItemType.TypeC, DateTime.Today, DateTime.Today.AddDays(1), 4, 8), //делится на 2 -- все проходят
                new Item(24, Types.ItemType.TypeA, DateTime.Today, DateTime.Today.AddDays(1), 3, 12), //делится на 3 -- все проходят
                new Item(23, Types.ItemType.TypeC, DateTime.Today, DateTime.Today.AddDays(1), 1, 18), //делится на 3 -- 1 проходят
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