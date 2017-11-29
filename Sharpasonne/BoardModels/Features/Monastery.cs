using System.Collections.Immutable;

namespace Sharpasonne.BoardModels
{
    class Monastery : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }
    }
}