using TestTask.Types;

namespace TestTask.Entities
{
    public class Item : ICloneable, IEquatable<Item?>
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

        public override bool Equals(object? obj)
        {
            return Equals(obj as Item);
        }

        public bool Equals(Item? other)
        {
            return other is not null &&
                   Weight == other.Weight &&
                   Type == other.Type &&
                   StartLiveTime == other.StartLiveTime &&
                   EndLiveTime == other.EndLiveTime &&
                   H1 == other.H1 &&
                   H2 == other.H2;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Weight, Type, StartLiveTime, EndLiveTime, H1, H2);
        }
    }
}
