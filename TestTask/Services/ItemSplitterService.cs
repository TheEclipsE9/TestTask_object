using TestTask.Entities;

namespace TestTask.Services
{
    public class ItemSplitterService
    {
        public bool ShouldBeSplitted(Item item, double windowBottom, double windowTop)
        {
            return item.H2 - item.H1 > windowTop - windowBottom && (item.H1 < windowTop && item.H2 > windowBottom);
        }

        public List<Item> SplitItem(Item item, double windowBottom, double windowTop)
        {
            var splitedItems = new List<Item>();

            var parentItem = (Item)item.Clone();

            if (item.H2 > windowTop)
            {
                var upperItem = (Item)item.Clone();
                upperItem.H1 = windowTop;
                splitedItems.Add(upperItem);

                parentItem.H2 = windowTop;
            }
            if (item.H1 < windowBottom)
            {
                var bottomItem = (Item)item.Clone();
                bottomItem.H2 = windowBottom;
                splitedItems.Add(bottomItem);

                parentItem.H1 = windowBottom;
            }
            splitedItems.Add(parentItem);

            CalculateWeight(splitedItems, item);

            return splitedItems;
        }

        private void CalculateWeight(List<Item> itemsList, Item parentItem)
        {
            foreach (var item in itemsList)
            {
                var n = (parentItem.H2 - parentItem.H1) / (item.H2 - item.H1);
                item.Weight = item.Weight / n;
            }
        }
    }
}
