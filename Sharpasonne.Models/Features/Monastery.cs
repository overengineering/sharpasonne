using System.Collections.Immutable;

namespace Sharpasonne.Models.Features
{
    class Monastery : IFeature
    {
        public IImmutableSet<Segment> Connections { get; }
    }
}

