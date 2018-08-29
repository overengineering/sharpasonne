using System.Collections.Immutable;
using Optional;
using System.Collections.Generic;
using JetBrains.Annotations;
using System.Linq;

namespace Sharpasonne.Models
{
    /// <summary>
    /// Tiles in play.
    /// </summary>
    public class Board
    {
        [NotNull]
        private ImmutableDictionary<Point, Placement> Grid { get; } 
            = ImmutableDictionary.Create<Point, Placement>();

        public Board() { }

        public Board([NotNull] IDictionary<Point, Placement> grid)
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

        /// <summary>
        /// Adds a tile to the board. Doesn't modify the current board, rather
        /// create a new one.
        /// </summary>
        /// <param name="point">Where to place the tile on the board.</param>
        /// <returns>The new board.</returns>
        [NotNull]
        public Board Set([NotNull] Tile tile, Point point)
        {
            var placement = new Placement(tile);

            // TODO: change this to a try add and return an Option.
            var grid = this.Grid.Add(point, placement);

            return new Board(grid);
        }

        public IImmutableDictionary<Point, Placement> ToImmutableDictionary()
        {
            return this.Grid;
        }

        public IImmutableDictionary<Rotation, Option<Placement>> GetAdjecentTiles(Point point)
        {
            var adjecentTiles = new Dictionary<Rotation, Option<Placement>> {
                [Rotation.None] = Get(new Point(point.X, point.Y + 1)),
                [Rotation.Quarter] = Get(new Point(point.X + 1, point.Y)),
                [Rotation.Half] = Get(new Point(point.X, point.Y - 1)),
                [Rotation.ThreeQuarter] = Get(new Point(point.X - 1, point.Y)),
            }.ToImmutableDictionary();

            return adjecentTiles;
        }

        public IImmutableDictionary<Point, Option<Placement>> GetAdjecentPointsAndPlacements(Point point)
        {
            var adjecentPoints = new List<Point>
            {
                new Point(point.X, point.Y + 1),
                new Point(point.X + 1, point.Y),
                new Point(point.X, point.Y - 1),
                new Point(point.X - 1, point.Y),
            };
            var adjecentPointsAndPlacements = adjecentPoints
                .ToImmutableDictionary(p => p, p => Get(p));

            return adjecentPointsAndPlacements;
        }
    }
}
