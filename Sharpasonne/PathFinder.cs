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
        public Option<IImmutableDictionary<Tile, IImmutableList<IFeature>>> FindFeatureTiles(
            Point point, Board board, IFeature feature)
        {
            var placementOption = board.Get(point);
            return placementOption.Match(placement =>
            {
                var featureTiles = FindFeatureTilesRecursevely(new List<Point>(), point, board, placement, feature);

                return featureTiles.ToImmutableDictionary();
            },
            () => Option.None<IImmutableDictionary<Tile, IImmutableList<IFeature>>>());
        }

        private Dictionary<Tile, IFeature> FindFeatureTilesRecursevely(
            List<Point> exhaustedPoints, Point point, Board board, Placement placement, IFeature feature)
        {
            if(exhaustedPoints.Contains(point))
            {
                return new Dictionary<Tile, IFeature>();
            }
            exhaustedPoints.Add(point);
            
            var featureTiles = new Dictionary<Tile, IFeature>
            {
                [placement.Tile] = feature,
            };
            var adjecentTiles = board.GetAdjecentPointsAndPlacements(point);
            var adjecentFeatureTiles = adjecentTiles
                .Where(at => at.Value.HasValue)
                .Select(at =>
                    new KeyValuePair<Point, Placement>(at.Key, at.Value.ValueOrFailure()))
                .Select(at => FindFeatureTilesRecursevely(
                    exhaustedPoints, at.Key, board, at.Value));

        }
    }
}