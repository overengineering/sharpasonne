using System.Collections.Immutable;
using Optional;
using System.Collections.Generic;

namespace Sharpasonne.Models
{
    public class Board
    {
        private ImmutableDictionary<Point, Placement> Grid { get; } 
            = ImmutableDictionary.Create<Point, Placement>();

        public Board() { }

        public Board(IDictionary<Point, Placement> grid)
        {
            this.Grid = grid.ToImmutableDictionary();
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
            var placement = new Placement(new TilePlacement(tile, orientation));

            var grid = this.Grid.Add(point, placement);

            return new Board(grid);
        }

        public IImmutableDictionary<Point, Placement> ToImmutableDictionary()
        {
            return this.Grid;
        }

        public IImmutableDictionary<Orientation, Option<Placement>> GetAdjecentTiles(Point point)
        {
            var adjecentTiles = new Dictionary<Orientation, Option<Placement>> {
                [Orientation.Top] = Get(new Point(point.X, point.Y + 1)),
                [Orientation.Right] = Get(new Point(point.X + 1, point.Y)),
                [Orientation.Bottom] = Get(new Point(point.X, point.Y - 1)),
                [Orientation.Left] = Get(new Point(point.X - 1, point.Y)),
            }.ToImmutableDictionary();

            return adjecentTiles;
        }
    }
}
