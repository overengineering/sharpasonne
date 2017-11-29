using System.Collections.Immutable;

namespace Sharpasonne.BoardModels
{
    internal class Board
    {
        private IImmutableDictionary<Point, Placement> Grid { get; } 
            = ImmutableDictionary.Create<Point, Placement>();
    }
}
