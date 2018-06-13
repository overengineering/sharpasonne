using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Optional;
using Optional.Unsafe;
using Sharpasonne.Models;

namespace Sharpasonne
{
    public class PathFinder
    {
        public Option<IImmutableDictionary<IFeature, Tile>> FindFeatureTiles(
            Point point, Board board, IFeature feature)
        {
            var placementOption = board.Get(point);
            var findFeatureTiles = placementOption.Match(placement =>
                {
                    var featureTiles = FindFeatureTilesRecursevely(
                        new Dictionary<IFeature, Tile>(),
                        point,
                        board,
                        feature);

                    return Option.Some<IImmutableDictionary<IFeature, Tile>>(featureTiles.ToImmutableDictionary());
                },
                Option.None<IImmutableDictionary<IFeature, Tile>>);

            return findFeatureTiles;
        }

        // TODO: We need to find the features by segment for each feature and edge.
        private Dictionary<IFeature, Tile> FindFeatureTilesRecursevely(
            Dictionary<IFeature, Tile> featureTile,
            Point                      point,
            Board                      board,
            IFeature                   feature)
        {
            var optionalPlacement = board.Get(point);

            // If recursion has already been on this feature or no tile at this
            // point return the dictionary...
            if (featureTile.ContainsKey(feature) || !optionalPlacement.HasValue)
            {
                return featureTile;
            }

            var placement = optionalPlacement.ValueOrFailure();

            // ...otherwise add self and recurse with the adjecent tiles.
            featureTile.Add(feature, placement.TilePlacement.Tile);
            
            //feature.Connections.

            var possibleNeighbours = board.GetAdjecentPointsAndPlacements(point);
            var neighbours = possibleNeighbours
                .Where(at => at.Value.HasValue)
                .Select(at => new {
                    Point = at.Key,
                    Placement  = at.Value.ValueOrFailure(),
                });

            foreach (var neighbour in neighbours)
            {
                //tile.Tile.TilePlacement.GetFeaturesAt()

                FindFeatureTilesRecursevely(
                    // TODO: extract what on the adjecentFeatureMatchRule gives the matching edges.
                    featureTile,
                    neighbour.Point,
                    board,
                    neighbour.Placement.TilePlacement.Tile.Features.First());
            }

            return featureTile;
        }
    }
}