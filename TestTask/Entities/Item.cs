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
            if (h1 > h2)
            {
                throw new Exception("H2 can not be less than H1.");
            }
            if (h1 == h2)
            {
                throw new Exception("Height can not be 0.");
            }
            if (h1 < 0 && h2 < 0)
            {
                throw new Exception("H1 and H2 can not be negative");
            }

            Weight = weight;
            Type = type;
            StartLiveTime = startLiveTime;
            EndLiveTime = endLiveTime;
            H1 = h1;
            H2 = h2;
        }

        public double GetHeight()
        {
            return H2 - H1;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
