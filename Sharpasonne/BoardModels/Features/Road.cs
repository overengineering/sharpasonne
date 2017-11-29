using System.Collections.Immutable;

namespace Sharpasonne.BoardModels.Features
{
    internal class Road : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }
    }
}