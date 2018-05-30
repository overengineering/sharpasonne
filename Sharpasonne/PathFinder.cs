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
                    var featureTiles = FindFeatureTilesRecursevely(new Dictionary<IFeature, Tile>(), point, board,
                        placement,                                                                   feature);

                    return Option.Some<IImmutableDictionary<IFeature, Tile>>(featureTiles.ToImmutableDictionary());
                },
                Option.None<IImmutableDictionary<IFeature, Tile>>);

            return findFeatureTiles;
        }

        private Dictionary<IFeature, Tile> FindFeatureTilesRecursevely(
            Dictionary<IFeature, Tile> featureTile,
            Point                      point,
            Board                      board,
            Placement                  placement,
            IFeature                   feature)
        {
            // If recursion has already been on this feature return the dictionary...
            if (featureTile.ContainsKey(feature))
            {
                return featureTile;
            }

            // ...otherwise add self and recurse with the adjecent tiles.
            featureTile.Add(feature, placement.TilePlacement.Tile);

            var adjecentTiles = board.GetAdjecentPointsAndPlacements(point);
            var adjecentFeatureTiles = adjecentTiles
                .Where(at => at.Value.HasValue)
                .Select(at =>
                    new KeyValuePair<Point, Placement>(at.Key, at.Value.ValueOrFailure()))
                .SelectMany(at => FindFeatureTilesRecursevely(
                    // TODO: extract what on the adjecentFeatureMatchRule gives the matching edges.
                    featureTile, at.Key, board, at.Value, feature))
                .ToDictionary(x => x.Key, x => x.Value);

            return adjecentFeatureTiles;
        }
    }
}