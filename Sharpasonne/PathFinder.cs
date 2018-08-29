using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
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
                    var featureTiles = FindFeatureTilesRecursively(
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
        private Dictionary<IFeature, Tile> FindFeatureTilesRecursively(
            Dictionary<IFeature, Tile> acc,
            Point                      point,
            Board                      board,
            IFeature                   feature)
        {
            var optionalPlacement = board.Get(point);

            // If recursion has already been on this feature or no tile at this
            // point return the dictionary...
            if (acc.ContainsKey(feature) || !optionalPlacement.HasValue)
            {
                return acc;
            }

            var placement = optionalPlacement.ValueOrFailure();

            // ...otherwise add self and recurse with the adjecent tiles.
            acc.Add(feature, placement.Tile);
            
            // TODO: Point utility funcs.
            acc = FindFeatureTilesByEdge(acc, board, feature, new Point(point.X,     point.Y + 1), Edge.Bottom);
            acc = FindFeatureTilesByEdge(acc, board, feature, new Point(point.X + 1, point.Y),     Edge.Left);
            acc = FindFeatureTilesByEdge(acc, board, feature, new Point(point.X,     point.Y - 1), Edge.Top);
            acc = FindFeatureTilesByEdge(acc, board, feature, new Point(point.X - 1, point.Y),     Edge.Right);

            return acc;
        }

        /// <summary>
        /// Add features from neighbouring tile on a specified edge.
        /// </summary>
        private Dictionary<IFeature, Tile> FindFeatureTilesByEdge(
            Dictionary<IFeature, Tile> acc,
            Board board,
            IFeature feature,
            Point nextPoint,
            Edge nextEdge)
        {
            var edgeSegments =
                SegmentConstants.SegmentEdges[nextEdge.RotateClockwise(Rotation.Half)];

            var featureSegments = feature.Connections.Intersect(edgeSegments);
            if (!featureSegments.Any())
            {
                return acc;
            }

            var tilePlacementOpt = board.Get(nextPoint);
            tilePlacementOpt.MatchSome(p =>
            {
                var features = p.Tile.GetEdge(nextEdge);
                var matchingFeatures = featureSegments
                    .Select(s => SegmentConstants.AdjacentSegments[s])
                    .Select(s => features.FirstOrDefault(f => f.Connections.Contains(s)))
                    .Distinct();
                foreach (var matchingFeature in matchingFeatures)
                {
                    FindFeatureTilesRecursively(acc, nextPoint, board, matchingFeature);
                }
            });

            return acc;
        }
    }
}