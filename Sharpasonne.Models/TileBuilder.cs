using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using Optional;

namespace Sharpasonne.Models
{
    public class TileBuilder
    {
        public Option<Tile> CreateTile([NotNull] IEnumerable<IFeature> features)
        {
            var featureList = features as IImmutableList<IFeature> ?? features.ToImmutableList();

            if (new FeatureListValidator().IsValid(featureList))
            {
                var tile = new Tile(featureList);
                return Option.Some(tile);
            }
            
            return Option.None<Tile>();
        }
    }
}
