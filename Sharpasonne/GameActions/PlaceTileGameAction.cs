using Sharpasonne;
using Sharpasonne.Models;

namespace Sharpasonne.GameActions
{
    public class PlaceTileGameAction : IGameAction
    {
        public Placement Placement { get; }
        public Point Point { get; }

        public PlaceTileGameAction(Point point, Placement placement)
        {
            this.Point = point;
            this.Placement = placement;
        }
    }
}
