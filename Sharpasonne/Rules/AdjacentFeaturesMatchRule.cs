using System;
using System.Linq;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Optional.Unsafe;

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
                    o.Value.ValueOrFailure()
                ))
                .All(o =>
                {
                    (var direction, var placement) = o;

                    switch (direction)
                    {
                        case Orientation.Top:
                        {
                            return this.EdgesMatch(
                                placement.TilePlacement.GetEdge(Orientation.Bottom),
                                action.Placement.GetEdge(Orientation.Top)
                            );
                        }
                        case Orientation.Bottom:
                        {
                            return this.EdgesMatch(
                                placement.TilePlacement.GetEdge(Orientation.Top),
                                action.Placement.GetEdge(Orientation.Bottom)
                            );
                        }
                        case Orientation.Left:
                        {
                            return this.EdgesMatch(
                                placement.TilePlacement.GetEdge(Orientation.Right),
                                action.Placement.GetEdge(Orientation.Left)
                            );
                        }
                        case Orientation.Right:
                        {
                            return this.EdgesMatch(
                                placement.TilePlacement.GetEdge(Orientation.Left),
                                action.Placement.GetEdge(Orientation.Right)
                            );
                        }

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
