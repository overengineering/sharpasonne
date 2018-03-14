namespace Sharpasonne.Models
{
    public class TilePlacement
    {
        public Tile        Tile        { get; }
        public Orientation Orientation { get; }

        public TilePlacement(Tile tile, Orientation orientation)
        {
            Tile        = tile;
            Orientation = orientation;
        }

        public IFeature[] GetEdge(Orientation orientation)
        {
            var direction = orientation.RotateInverse(this.Orientation);
            return this.Tile.GetEdge(direction);
        }
    }
}