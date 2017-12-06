using System.Collections.Generic;
using System.Collections.Immutable;
using JetBrains.Annotations;

namespace Sharpasonne.Models
{
    public class Tile
    {
        public IImmutableList<IFeature> Features { get; }

        internal Tile([NotNull] IEnumerable<IFeature> features)
        {
            this.Features = features.ToImmutableList();
        }
    }
}
