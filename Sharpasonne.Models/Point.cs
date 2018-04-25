namespace Sharpasonne.Models
{
    /// <summary>
    /// Everyone ends up making a point. This one represents a location on a
    /// <see cref="Board"/>.
    /// </summary>
    public struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}

