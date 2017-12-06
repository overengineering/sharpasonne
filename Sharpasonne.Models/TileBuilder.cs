using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using Optional;
using Sharpasonne.Models;

namespace Sharpasonne
{
    public class TileBuilder
    {
        public Option<Tile> CreateTile([NotNull] IEnumerable<IFeature> features)
        {
            var featureList = features as IImmutableList<IFeature> ?? features.ToImmutableList();

            if (IsValid(featureList))
            {
                var tile = new Tile(featureList);
                return Option.Some(tile);
            }
            
            return Option.None<Tile>();
        }

        private bool IsValid([NotNull] IImmutableList<IFeature> featureList)
        {
            // TODO: It.
            return true;
        }
    }
}
