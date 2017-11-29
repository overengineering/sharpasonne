interface IRule
{
    bool match(Board board, Placement placement, Tile tile);
}