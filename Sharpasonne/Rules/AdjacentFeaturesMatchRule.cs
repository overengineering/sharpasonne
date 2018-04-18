using System;
using System.Linq;
using Sharpasonne.GameActions;
using Sharpasonne.Models;
using Optional.Unsafe;

namespace Sharpasonne.Rules
{
    /// <summary>
    /// Ensures that the type of features on all edges of the tile to place
    /// match the corresponding features on the surrounding tiles.
    /// </summary>
    public class AdjacentFeaturesMatchRule : IRule<PlaceTileGameAction>
    {
        public bool Verify<T1>(IEngine engine, T1 action)
            where T1 : PlaceTileGameAction
        {
            var adjacent = engine.Board.GetAdjecentTiles(action.Point);
            var allMatch = adjacent
                // Where orientation has a Placement
                .Where(kv => kv.Value.HasValue)
                .Select(kv => (
                    kv.Key,
                    // Never failure due to Where.
                    kv.Value.ValueOrFailure() 
                ))
                .All(kv =>
                {
                    (var direction, var placement) = kv;

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

        /// <summary>
        /// Checks that the order of the types implementing <see cref="IFeatures"/> 
        /// are aligned, thereby showing that the edges are matched.
        /// </summary>
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
