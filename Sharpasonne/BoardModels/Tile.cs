using System.Collections.Immutable;

namespace Sharpasonne.BoardModels
{
    internal class Tile
    {
        public IImmutableList<IFeature> Features { get; }
    }
}
