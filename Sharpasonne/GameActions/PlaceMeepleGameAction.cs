using Sharpasonne.Models;

namespace Sharpasonne.GameActions
{
    /*public class PlaceMeepleGameAction : IGameAction
    {
        public MeeplePlacement MeeplePlacement { get; }
        public Point Point { get; }

        public PlaceMeepleGameAction(Point point, MeeplePlacement meeplePlacement)
        {
            this.Point = point;
            this.MeeplePlacement = meeplePlacement;
        }

        public IEngine Perform(IEngine engine)
        {

            var board = engine.Board.Set(MeeplePlacement.Tile, Point, meeplePlacement.Orientation);
            return new EngineState(board, engine.Rules);
        }
    }*/
}
