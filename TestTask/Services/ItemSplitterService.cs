using TestTask.Entities;

namespace TestTask.Services
{
    public class ItemSplitterService
    {
        public bool ShouldBeSplitted()
        {
            return true;
        }

        public List<Item> SplitItem()
        {
            return null;
        }

        private double CalculateWeight()
        {
            return 0;
        }
    }
}
