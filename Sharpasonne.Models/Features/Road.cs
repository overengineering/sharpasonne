using System.Collections.Immutable;

namespace Sharpasonne.Models.Features
{
    internal class Road : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }
    }
}

