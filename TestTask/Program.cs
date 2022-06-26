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
                new Item(600, Types.ItemType.TypeA, DateTime.Now, DateTime.Now.AddMinutes(10), 2, 6),
                new Item(15, Types.ItemType.TypeB, DateTime.Now, DateTime.Now.AddMinutes(10), 1, 4),
                new Item(20, Types.ItemType.TypeC, DateTime.Now, DateTime.Now.AddMinutes(10), 6, 10),
                new Item(22, Types.ItemType.TypeA, DateTime.Now, DateTime.Now.AddMinutes(10), 4, 8),
                new Item(23, Types.ItemType.TypeC, DateTime.Now, DateTime.Now.AddMinutes(10), 3, 11),
            };
            var window = new Window(6, 9);

            
            var _itemSplitterService = new ItemSplitterService();
            var _correctItemPickerService = new CorrectItemPickerService(_itemSplitterService);

            var result = new List<Item>();

            result = _correctItemPickerService.GetCorrectItemsWithoutSplitting(itemList, window.Bottom, window.Top);
            foreach (var item in result)
            {
                Console.WriteLine($"Type: { item.Type} | H1: {item.H1} | H2: {item.H2} | Weight: {item.Weight}");
            }
            Console.WriteLine("-----------------");
            Console.WriteLine("#################");
            Console.WriteLine("-----------------");

            result = _correctItemPickerService.GetCorrectItemsWithSplitting(itemList, window.Bottom, window.Top);
            foreach (var item in result)
            {
                Console.WriteLine($"Type: {item.Type}| H1: {item.H1} | H2: {item.H2} | Weight: {item.Weight}");
            }
        }
    }
}