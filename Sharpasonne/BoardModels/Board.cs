using System.Collections.Immutable;

namespace Sharpasonne.BoardModels
{
    internal class Board
    {
        ImmutableList<Tile> Tiles { get; set; }
    }
}
