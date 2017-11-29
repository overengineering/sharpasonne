using System.Collections.Immutable;

namespace Sharpasonne.BoardModels
{
    class City : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }
        public bool HasShield { get; }
    }
}