using TestTask.Types;

namespace TestTask.Entities
{
    public class Item : ICloneable
    {
        public double Weight { get; set; }
        public ItemType Type { get; set; }
        public DateTime StartLiveTime { get; set; }
        public DateTime EndLiveTime { get; set; }
        public double H1 { get; set; }
        public double H2 { get; set; }

        public Item(double weight, ItemType type, DateTime startLiveTime, DateTime endLiveTime, double h1, double h2)
        {
            Weight = weight;
            Type = type;
            StartLiveTime = startLiveTime;
            EndLiveTime = endLiveTime;
            H1 = h1;
            H2 = h2;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
