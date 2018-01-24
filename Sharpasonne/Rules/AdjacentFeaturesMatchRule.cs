using System;
using System.Collections;
using System.Linq;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using System.Collections.Generic;

namespace Sharpasonne.Rules
{
    public class AdjacentFeaturesMatchRule : IRule<PlaceTileGameAction>
    {
        public bool Verify<T1>(IEngine engine, T1 action)
            where T1 : PlaceTileGameAction
        {
            var adjacent = engine.Board.GetAdjecentTiles(action.Point);
            var allMatch = adjacent
                .Where(o => o.Value.HasValue)
                .Select(o => (
                    o.Key,
                    o.Value.ValueOr(null as Placement)
                ))
                .All(o =>
                {
                    (var direction, var placement) = o;

                    switch (direction)
                    {
                        case Orientation.Top:
                            return this.EdgesMatch(
                                placement.Tile.GetEdge(Orientation.Bottom),
                                action.Placement.Tile.GetEdge(Orientation.Top)
                            );
                        default:
                            throw new NotImplementedException();
                    }
                });

            return allMatch;
        }

        protected bool EdgesMatch(
            IFeature[] from,
            IFeature[] to
        )
        {
            var fromTypes = from.Select(feature => feature.GetType());
            var toTypes = to.Select(feature => feature.GetType());

            return fromTypes.SequenceEqual(toTypes);
        }
    }
}
