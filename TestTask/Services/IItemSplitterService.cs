using TestTask.Entities;

namespace TestTask.Services
{
    public interface IItemSplitterService
    {
        public bool ShouldBeSplitted(Item item, double windowBottom, double windowTop);
        public List<Item> SplitItem(Item item, double windowBottom, double windowTop);
    }
}
