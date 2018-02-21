using System.Linq;
using Sharpasonne.GameActions;

namespace Sharpasonne.Rules
{
    public class UniqueTileInstanceRule : IRule<PlaceTileGameAction>
    {
        public bool Verify<T1>(IEngine engine, T1 gameAction) where T1 : PlaceTileGameAction
        {
            var allTiles = engine.Board
                .ToImmutableDictionary()
                .Values
                .Select(placement => placement.TilePlacement.Tile);

            return !allTiles
                .Contains(gameAction.Placement.Tile);
        }
    }
}
