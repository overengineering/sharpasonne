using System.Collections.Immutable;

namespace Sharpasonne.BoardModels
{
    public class Tile
    {
        public IImmutableList<IFeature> Features { get; }
    }
}
