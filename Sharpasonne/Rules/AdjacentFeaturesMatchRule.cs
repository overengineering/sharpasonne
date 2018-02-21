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
            var adjacent = engine.Board.GetAdjecentOrientationAndPlacements(action.Point);
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
                        case Rotation.None:
                        {
                            return this.EdgesMatch(
                                placement.TilePlacement.GetFeaturesAt(Edge.Bottom),
                                action.Placement.GetFeaturesAt(Edge.Top)
                            );
                        }
                        case Rotation.Half:
                        {
                            return this.EdgesMatch(
                                placement.TilePlacement.GetFeaturesAt(Edge.Top),
                                action.Placement.GetFeaturesAt(Edge.Bottom)
                            );
                        }
                        case Rotation.ThreeQuarter:
                        {
                            return this.EdgesMatch(
                                placement.TilePlacement.GetFeaturesAt(Edge.Right),
                                action.Placement.GetFeaturesAt(Edge.Left)
                            );
                        }
                        case Rotation.Quarter:
                        {
                            return this.EdgesMatch(
                                placement.TilePlacement.GetFeaturesAt(Edge.Left),
                                action.Placement.GetFeaturesAt(Edge.Right)
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
