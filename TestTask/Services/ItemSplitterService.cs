using TestTask.Entities;

namespace TestTask.Services
{
    public class ItemSplitterService : IItemSplitterService
    {
        public bool ShouldBeSplitted(Item item, double windowBottom, double windowTop)
        {            
            var canBeSplittedByBottomBorder = item.H1 < windowBottom && item.H2 > windowBottom;
            var canBeSplittedByTopBorder = item.H2 > windowTop && item.H1 < windowTop;
            return canBeSplittedByBottomBorder || canBeSplittedByTopBorder;
        }

        public List<Item> SplitItem(Item item, double windowBottom, double windowTop)
        {
            var parentItem = (Item)item.Clone();
            var splitedItems = new List<Item> { parentItem };
            
            if (item.H2 > windowTop && item.H1 < windowTop)
            {
                var upperItem = (Item)item.Clone();
                upperItem.H1 = windowTop;
                splitedItems.Add(upperItem);

                parentItem.H2 = windowTop;
            }
            if (item.H1 < windowBottom && item.H2 > windowBottom)
            {
                var bottomItem = (Item)item.Clone();
                bottomItem.H2 = windowBottom;
                splitedItems.Add(bottomItem);

                parentItem.H1 = windowBottom;
            }
            
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
