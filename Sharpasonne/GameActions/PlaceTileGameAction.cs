using Sharpasonne.Models;

namespace Sharpasonne.GameActions
{
    /// <inheritdoc />
    public class PlaceTileGameAction : IGameAction
    {
        public Tile Tile { get; }
        public Point Point { get; }

        public PlaceTileGameAction(Point point, Tile tile)
        {
            this.Point = point;
            this.Tile = tile;
        }

        public IEngine Perform(IEngine engine)
        {
            var board = engine.Board.Set(Tile, Point);
            return new EngineState(board, engine);
        }
    }
}
