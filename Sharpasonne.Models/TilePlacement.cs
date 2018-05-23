namespace Sharpasonne.Models
{
    /// <summary>
    /// A Tile when placed on a gameboard.
    /// </summary>
    public class TilePlacement
    {
        public Tile     Tile     { get; }
        public Rotation Rotation { get; }

        public TilePlacement(Tile tile, Rotation rotation)
        {
            Tile     = tile;
            Rotation = rotation;
        }

        /// <summary>
        /// Distinct features along the given edge in clockwise order.
        /// </summary>
        public IFeature[] GetFeaturesAt(Edge edge)
        {
            var direction = edge.RotateAntiClockwise(this.Rotation);
            return this.Tile.GetEdge(direction);
        }
    }
}