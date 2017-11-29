using System.Collections.Immutable;

namespace Sharpasonne.BoardModels
{
    internal class Road : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }
    }
}