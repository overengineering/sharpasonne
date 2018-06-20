using System;
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
            
            //feature.Connections.
            //var normalisedTile = new TileBuilder()
            //    .CreateTile(placement.TilePlacement.Tile.Features.ToArray());
            //foreach (var feature1 in normalisedTile.ValueOrFailure().Features)
            //{
            //    feature1.Connections = feature1.Connections.Select(c => c.rota)
            //}

            var segments = feature.Connections;
            // TODO: Extract segments to constants.
            var topFeatureSegments = segments.Intersect(new Segment[] {Segment.TopLeft, Segment.Top, Segment.TopRight});
            if (topFeatureSegments.Any())
            {
                // TODO: Point utility funcs.
                var pointAbove = new Point(point.X, point.Y + 1);
                FindFeatureTile(acc, board, pointAbove, topFeatureSegments, Edge.Bottom);
            }


            /*var possibleNeighbours = board.GetAdjecentPointsAndPlacements(point);
            var neighbours = possibleNeighbours
                .Where(at => at.Value.HasValue)
                .Select(at => new {
                    Point = at.Key,
                    Placement  = at.Value.ValueOrFailure(),
                });

            foreach (var neighbour in neighbours)
            {
                //tile.Tile.TilePlacement.GetFeaturesAt()
                //var segment = feature.Connections.

                FindFeatureTilesRecursevely(
                    // TODO: extract what on the adjecentFeatureMatchRule gives the matching edges.
                    acc,
                    neighbour.Point,
                    board,
                    neighbour.Placement.Tile.Features.First());
            }*/

            return acc;
        }

        private void FindFeatureTile(Dictionary<IFeature, Tile> acc, Board board, Point point, IImmutableSet<Segment> featureSegments, Edge edge)
        {
            var tilePlacementOpt = board.Get(point);
            tilePlacementOpt.MatchSome(placement =>
            {
                var features = placement.Tile.GetEdge(edge);
                var matchingFeatures = featureSegments
                    .Select(s => SegmentExtensions.AdjacentSegments[s])
                    .Select(s => features.FirstOrDefault(f => f.Connections.Contains(s)))
                    .Distinct();
                foreach (var matchingFeature in matchingFeatures)
                {
                    FindFeatureTilesRecursevely(acc, point, board, matchingFeature);
                }
            });
        }
    }
}