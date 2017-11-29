using System.Collections.Immutable;
using System.Numerics;

namespace Sharpasonne.BoardModels
{
    internal class Board
    {
        public IImmutableDictionary<Point, Placement> Grid { get; }
    }
}
