using System.Collections.Immutable;

namespace Sharpasonne.BoardModels.Features
{
    class Monastery : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }
    }
}