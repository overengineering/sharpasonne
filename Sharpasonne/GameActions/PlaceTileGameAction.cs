using Sharpasonne.Models;

namespace Sharpasonne.GameActions
{
    /// <inheritdoc />
    public class PlaceTileGameAction : IGameAction
    {
        public TilePlacement Placement { get; }
        public Point Point { get; }

        public PlaceTileGameAction(Point point, TilePlacement placement)
        {
            this.Point = point;
            this.Placement = placement;
        }

        public IEngine Perform(IEngine engine)
        {
            var board = engine.Board.Set(Placement.Tile, Point, Placement.Rotation);
            return new EngineState(board, engine);
        }
    }
}
