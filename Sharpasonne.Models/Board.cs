using System.Collections.Immutable;
using Optional;

namespace Sharpasonne.Models
{
    public class Board
    {
        private IImmutableDictionary<Point, Placement> Grid { get; } 
            = ImmutableDictionary.Create<Point, Placement>();

        public Board() { }

        public Board(IImmutableDictionary<Point, Placement> grid)
        {
            this.Grid = grid;
        }

        public Option<Placement> Get(Point point)
        {
            var placement = Grid.TryGetValue(point, out var value) 
                ? Option.Some(value) 
                : Option.None<Placement>();

            return placement;
        }

        public Board Set(Tile tile, Point point, Orientation orientation)
        {
            var placement = new Placement(tile, orientation);

            var grid = this.Grid.Add(point, placement);

            return new Board(grid);
        }

        public IImmutableDictionary<Point, Placement> ToImmutableDictionary()
        {
            return this.Grid;
        }
    }
}
