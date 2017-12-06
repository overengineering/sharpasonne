namespace Sharpasonne.BoardModels
{
    public class Placement
    {
        public Tile Tile { get; }
        public Orientation Orientation { get; }

        public Placement(Tile tile, Orientation orientation)
        {
            Tile = tile;
            Orientation = orientation;
        }
    }
}
