using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sharpasonne;
using Sharpasonne.GameActions;
using Sharpasonne.Models;

namespace Sharpasonne.Rules
{
    public class SpaceIsEmptyRule : IRule<PlaceTileGameAction>
    {
        public bool Verify<T>(IEngine engine, T gameAction)
            where T : PlaceTileGameAction
        {
            var optionalTile = engine.Board.Get(gameAction.Point);

            return !optionalTile.HasValue;
        }
    }
}
