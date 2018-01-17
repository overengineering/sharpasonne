using System.Collections.Immutable;

namespace Sharpasonne.Models.Features
{
    class City : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }
        public bool HasShield { get; }
    }
}

