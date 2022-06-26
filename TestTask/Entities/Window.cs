namespace TestTask.Entities
{
    public class Window
    {
        public double Bottom { get; set; }
        public double Top { get; set; }

        public Window(double bottom, double top)
        {
            Bottom = bottom;
            Top = top;
        }

        public double GetWindowHeight()
        {
            return Top - Bottom;
        }
    }
}
