namespace TestTask.Entities
{
    public class Window
    {
        public double Bottom { get; set; }
        public double Top { get; set; }

        public Window(double bottom, double top)
        {
            if (bottom > top)
            {
                throw new Exception("TopBorder can not be lower than BottomBorder.");
            }
            if (bottom == top)
            {
                throw new Exception("Window height can not be 0.");
            }
            if (bottom < 0 && top < 0)
            {
                throw new Exception("BottomBorder and TopBorder can not be negative");
            }

            Bottom = bottom;
            Top = top;
        }
    }
}
