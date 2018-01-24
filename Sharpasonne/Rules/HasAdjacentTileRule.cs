using Sharpasonne.GameActions;
using System.Linq;

namespace Sharpasonne.Rules
{
    public class HasAdjacentTileRule : IRule<PlaceTileGameAction>
    {
        public bool Verify<T1>(IEngine engine, T1 gameAction) where T1 : PlaceTileGameAction
        {
            var isValid = engine.Board.ToImmutableDictionary().Count() == 0
                || engine.Board.GetAdjecentTiles(gameAction.Point)
                    .Values
                    .Any(optionalTile => optionalTile.HasValue);

            return isValid;
        }
    }
}
