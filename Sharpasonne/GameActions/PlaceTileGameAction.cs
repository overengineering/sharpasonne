using Sharpasonne.Models;

namespace Sharpasonne.GameActions
{
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
            var board = engine.Board.Set(Placement.Tile, Point, Placement.Orientation);
            return new EngineState(board, engine);
        }
    }
    
    public class EngineState : IEngine
    {
        public EngineState(Board board, IEngine engine)
        {
            this.Board = board;
            this.Rules = engine.Rules;
            this.Players = engine.Players;
            this.CurrentPlayerTurn = engine.CurrentPlayerTurn;
        }

        public Board Board { get; }
        public Players Players { get; }
        public int CurrentPlayerTurn { get; }
        public IImmutableDictionary<Type, IImmutableList<IRule<IGameAction>>> Rules { get; }
    }
}
