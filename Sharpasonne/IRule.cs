using Sharpasonne.BoardModels;

namespace Sharpasonne
{
    internal interface IRule
    {
        bool Match(Board board, Placement placement, Tile tile);
    }
}
